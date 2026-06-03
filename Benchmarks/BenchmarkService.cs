using MerkleCloudIntegrity.Services;

namespace MerkleCloudIntegrity.Benchmarks;

/// <summary>
/// Defines benchmark entry points for Merkle Tree experiments.
/// </summary>
public sealed class BenchmarkService
{
    private readonly MerkleTreeService _merkleTreeService;
    private readonly VerificationService _verificationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BenchmarkService"/> class.
    /// </summary>
    /// <param name="merkleTreeService">The Merkle Tree service to benchmark.</param>
    /// <param name="verificationService">The verification service to benchmark.</param>
    public BenchmarkService(MerkleTreeService merkleTreeService, VerificationService verificationService)
    {
        _merkleTreeService = merkleTreeService;
        _verificationService = verificationService;
    }

    /// <summary>
    /// Executes the initial Phase-1 benchmark workflow.
    /// </summary>
    /// <param name="filePath">The file path used as benchmark input.</param>
    /// <param name="blockSize">The block size used for splitting.</param>
    public void RunPhaseOneBenchmark(string filePath, int blockSize)
    {
        throw new NotImplementedException();
    }
}
