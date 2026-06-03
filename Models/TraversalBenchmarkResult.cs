namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents benchmark measurements for a Merkle proof traversal strategy.
/// </summary>
public sealed class TraversalBenchmarkResult
{
    /// <summary>
    /// Gets or sets the traversal mode used by the benchmark.
    /// </summary>
    public string TraversalMode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the benchmarked file name.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of blocks generated from the file.
    /// </summary>
    public int BlockCount { get; set; }

    /// <summary>
    /// Gets or sets the elapsed proof generation time in milliseconds.
    /// </summary>
    public double ProofGenerationTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the elapsed proof verification time in milliseconds.
    /// </summary>
    public double VerificationTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the measured traversal memory usage in bytes.
    /// </summary>
    public long MemoryUsageBytes { get; set; }
}
