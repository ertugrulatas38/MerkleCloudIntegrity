namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents benchmark measurements for a Merkle Tree file integrity workflow.
/// </summary>
public sealed class BenchmarkResult
{
    /// <summary>
    /// Gets or sets the benchmarked file name.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the block size used during file splitting.
    /// </summary>
    public int BlockSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of file blocks.
    /// </summary>
    public int BlockCount { get; set; }

    /// <summary>
    /// Gets or sets the elapsed Merkle Tree build time in milliseconds.
    /// </summary>
    public double BuildTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the elapsed proof generation time in milliseconds.
    /// </summary>
    public double ProofGenerationTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the elapsed proof verification time in milliseconds.
    /// </summary>
    public double VerificationTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the measured memory usage in bytes.
    /// </summary>
    public long MemoryUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the generated Merkle Root hash.
    /// </summary>
    public string MerkleRoot { get; set; } = string.Empty;
}
