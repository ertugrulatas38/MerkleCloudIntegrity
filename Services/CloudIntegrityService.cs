using System.Text;
using MerkleCloudIntegrity.Models;

namespace MerkleCloudIntegrity.Services;

/// <summary>
/// Simulates cloud-side file block storage and Merkle proof based integrity checking.
/// </summary>
public sealed class CloudIntegrityService
{
    /// <summary>
    /// Defines the default file block size used by the simulation.
    /// </summary>
    public const int DefaultBlockSize = 1024;

    private readonly MerkleTreeService _merkleTreeService;
    private readonly VerificationService _verificationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CloudIntegrityService"/> class.
    /// </summary>
    /// <param name="merkleTreeService">The Merkle Tree service used to build roots and proofs.</param>
    /// <param name="verificationService">The verification service used to validate proofs.</param>
    public CloudIntegrityService(MerkleTreeService merkleTreeService, VerificationService verificationService)
    {
        _merkleTreeService = merkleTreeService;
        _verificationService = verificationService;
    }

    /// <summary>
    /// Runs a file integrity simulation by storing blocks, proving one block, and detecting tampering.
    /// </summary>
    /// <param name="filePath">The path of the file used for the simulation.</param>
    /// <param name="targetBlockIndex">The block index selected for integrity verification.</param>
    /// <param name="blockSize">The file block size in bytes.</param>
    /// <returns>The integrity simulation result.</returns>
    public CloudIntegritySimulationResult RunFileBlockIntegritySimulation(
        string filePath,
        int targetBlockIndex,
        int blockSize = DefaultBlockSize)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        var fileBlocks = _merkleTreeService.SplitFile(filePath, blockSize);
        var cloudBlocks = StoreBlocksInCloud(fileBlocks);
        ValidateTargetBlockIndex(cloudBlocks, targetBlockIndex);

        var root = _merkleTreeService.BuildTree(cloudBlocks);
        var originalMerkleRoot = _merkleTreeService.GetMerkleRoot(root);
        var proof = _merkleTreeService.GenerateProof(root, targetBlockIndex);
        var originalBlock = cloudBlocks[targetBlockIndex];
        var originalVerification = _verificationService.VerifyProof(originalBlock, proof, originalMerkleRoot);

        var tamperedBlock = CreateTamperedBlock(originalBlock);
        var tamperedVerification = _verificationService.VerifyProof(tamperedBlock, proof, originalMerkleRoot);

        return new CloudIntegritySimulationResult
        {
            FileBlockCount = cloudBlocks.Count,
            OriginalMerkleRoot = originalMerkleRoot,
            TargetBlockIndex = targetBlockIndex,
            OriginalVerificationResult = originalVerification,
            TamperedVerificationResult = tamperedVerification
        };
    }

    private static List<DataBlock> StoreBlocksInCloud(IReadOnlyList<DataBlock> fileBlocks)
    {
        return fileBlocks
            .Select(block => new DataBlock
            {
                Index = block.Index,
                Content = block.Content.ToArray(),
                Hash = block.Hash
            })
            .ToList();
    }

    private static void ValidateTargetBlockIndex(IReadOnlyList<DataBlock> blocks, int targetBlockIndex)
    {
        if (blocks.Count == 0)
        {
            throw new InvalidOperationException("The source file did not produce any data blocks.");
        }

        if (targetBlockIndex < 0 || targetBlockIndex >= blocks.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(targetBlockIndex),
                $"Target block index must be between 0 and {blocks.Count - 1}.");
        }
    }

    private static DataBlock CreateTamperedBlock(DataBlock originalBlock)
    {
        var tamperedContent = originalBlock.Content
            .Concat(Encoding.UTF8.GetBytes("-tampered"))
            .ToArray();

        return new DataBlock
        {
            Index = originalBlock.Index,
            Content = tamperedContent,
            Hash = originalBlock.Hash
        };
    }
}
