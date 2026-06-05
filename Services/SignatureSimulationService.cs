using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using MerkleCloudIntegrity.Models;

namespace MerkleCloudIntegrity.Services;

/// <summary>
/// Runs the hash-based signature experiment over a Merkle tree of one-time leaf values.
/// </summary>
public sealed class SignatureSimulationService
{
    private const int DefaultLeafCount = 1024;
    private const int DefaultLeafSizeBytes = 32;

    private readonly MerkleTreeService _merkleTreeService;
    private readonly VerificationService _verificationService;
    private readonly TraversalService _traversalService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SignatureSimulationService"/> class.
    /// </summary>
    /// <param name="merkleTreeService">The Merkle Tree service used to build the public tree.</param>
    /// <param name="verificationService">The verification service used to validate signatures.</param>
    /// <param name="traversalService">The traversal service used to generate authentication paths.</param>
    public SignatureSimulationService(
        MerkleTreeService merkleTreeService,
        VerificationService verificationService,
        TraversalService traversalService)
    {
        _merkleTreeService = merkleTreeService;
        _verificationService = verificationService;
        _traversalService = traversalService;
    }

    /// <summary>
    /// Runs the signature experiment with classical and cached authentication-path generation.
    /// </summary>
    /// <param name="csvPath">The CSV export path.</param>
    /// <param name="leafIndex">The leaf index selected as the simulated private key.</param>
    /// <param name="leafCount">The number of leaves in the simulated signature tree.</param>
    /// <returns>The generated simulation results.</returns>
    public IReadOnlyList<SignatureSimulationResult> RunSignatureSimulation(
        string csvPath,
        int leafIndex = 2,
        int leafCount = DefaultLeafCount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(csvPath);

        var blocks = GeneratePrivateKeyLeaves(leafCount);
        ValidateLeafIndex(blocks.Count, leafIndex);

        var root = _merkleTreeService.BuildTree(blocks);
        var results = new List<SignatureSimulationResult>
        {
            RunClassicalSimulation(blocks, root, leafIndex)
        };

        _traversalService.ClearCache();
        results.Add(RunCachedSimulation(blocks, root, leafIndex));

        ExportToCsv(results, csvPath);
        return results;
    }

    /// <summary>
    /// Writes signature measurements in the column order used by the paper tables.
    /// </summary>
    /// <param name="results">The simulation results to export.</param>
    /// <param name="csvPath">The output CSV path.</param>
    /// <returns>The generated CSV path.</returns>
    public string ExportToCsv(IReadOnlyList<SignatureSimulationResult> results, string csvPath)
    {
        ArgumentNullException.ThrowIfNull(results);
        ArgumentException.ThrowIfNullOrWhiteSpace(csvPath);

        var directory = Path.GetDirectoryName(csvPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var csv = new StringBuilder();
        csv.AppendLine("TraversalMode,LeafIndex,AuthenticationPathLength,SignatureGenerationTimeMs,SignatureVerificationTimeMs,MemoryUsageBytes");

        foreach (var result in results)
        {
            csv.AppendLine(string.Join(
                ',',
                result.TraversalMode,
                result.LeafIndex.ToString(CultureInfo.InvariantCulture),
                result.AuthenticationPathLength.ToString(CultureInfo.InvariantCulture),
                result.SignatureGenerationTimeMs.ToString("F4", CultureInfo.InvariantCulture),
                result.SignatureVerificationTimeMs.ToString("F4", CultureInfo.InvariantCulture),
                result.MemoryUsageBytes.ToString(CultureInfo.InvariantCulture)));
        }

        File.WriteAllText(csvPath, csv.ToString(), Encoding.UTF8);
        return csvPath;
    }

    private SignatureSimulationResult RunClassicalSimulation(
        IReadOnlyList<DataBlock> blocks,
        MerkleNode root,
        int leafIndex)
    {
        return RunMeasuredSimulation(
            "Classical",
            blocks,
            root,
            leafIndex,
            () => _traversalService.GenerateClassicalProof(root, leafIndex));
    }

    private SignatureSimulationResult RunCachedSimulation(
        IReadOnlyList<DataBlock> blocks,
        MerkleNode root,
        int leafIndex)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        var memoryBefore = GC.GetTotalMemory(forceFullCollection: true);

        // The first access fills the authentication-path cache; the measured call below tests reuse.
        _traversalService.GenerateCachedProof(root, leafIndex);

        var stopwatch = Stopwatch.StartNew();
        var signature = GenerateSignature(
            blocks[leafIndex],
            _traversalService.GenerateCachedProof(root, leafIndex));
        stopwatch.Stop();
        var generationTimeMs = stopwatch.Elapsed.TotalMilliseconds;

        stopwatch.Restart();
        _verificationService.VerifyProof(signature.LeafBlock, signature.AuthenticationPath, root.Hash);
        stopwatch.Stop();
        var verificationTimeMs = stopwatch.Elapsed.TotalMilliseconds;

        var memoryAfter = GC.GetTotalMemory(forceFullCollection: true);

        return CreateResult(
            "Cached",
            signature,
            generationTimeMs,
            verificationTimeMs,
            Math.Max(0, memoryAfter - memoryBefore));
    }

    private SignatureSimulationResult RunMeasuredSimulation(
        string traversalMode,
        IReadOnlyList<DataBlock> blocks,
        MerkleNode root,
        int leafIndex,
        Func<IReadOnlyList<ProofItem>> proofFactory)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        var memoryBefore = GC.GetTotalMemory(forceFullCollection: true);

        var stopwatch = Stopwatch.StartNew();
        var signature = GenerateSignature(blocks[leafIndex], proofFactory());
        stopwatch.Stop();
        var generationTimeMs = stopwatch.Elapsed.TotalMilliseconds;

        stopwatch.Restart();
        _verificationService.VerifyProof(signature.LeafBlock, signature.AuthenticationPath, root.Hash);
        stopwatch.Stop();
        var verificationTimeMs = stopwatch.Elapsed.TotalMilliseconds;

        var memoryAfter = GC.GetTotalMemory(forceFullCollection: true);

        return CreateResult(
            traversalMode,
            signature,
            generationTimeMs,
            verificationTimeMs,
            Math.Max(0, memoryAfter - memoryBefore));
    }

    private static MerkleSignature GenerateSignature(DataBlock leafBlock, IReadOnlyList<ProofItem> authenticationPath)
    {
        return new MerkleSignature
        {
            LeafIndex = leafBlock.Index,
            LeafBlock = leafBlock,
            AuthenticationPath = authenticationPath
        };
    }

    private static SignatureSimulationResult CreateResult(
        string traversalMode,
        MerkleSignature signature,
        double generationTimeMs,
        double verificationTimeMs,
        long memoryUsageBytes)
    {
        return new SignatureSimulationResult
        {
            TraversalMode = traversalMode,
            LeafIndex = signature.LeafIndex,
            AuthenticationPathLength = signature.AuthenticationPath.Count,
            SignatureGenerationTimeMs = generationTimeMs,
            SignatureVerificationTimeMs = verificationTimeMs,
            MemoryUsageBytes = memoryUsageBytes
        };
    }

    private static IReadOnlyList<DataBlock> GeneratePrivateKeyLeaves(int leafCount)
    {
        if (leafCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(leafCount), "Leaf count must be greater than zero.");
        }

        return Enumerable.Range(0, leafCount)
            .Select(index =>
            {
                var content = new byte[DefaultLeafSizeBytes];
                RandomNumberGenerator.Fill(content);

                return new DataBlock
                {
                    Index = index,
                    Content = content
                };
            })
            .ToList();
    }

    private static void ValidateLeafIndex(int leafCount, int leafIndex)
    {
        if (leafIndex < 0 || leafIndex >= leafCount)
        {
            throw new ArgumentOutOfRangeException(
                nameof(leafIndex),
                $"Leaf index must be between 0 and {leafCount - 1}.");
        }
    }
}
