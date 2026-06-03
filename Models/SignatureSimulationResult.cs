namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents benchmark measurements for a simulated Merkle-based signature workflow.
/// </summary>
public sealed class SignatureSimulationResult
{
    /// <summary>
    /// Gets or sets the traversal mode used for authentication path generation.
    /// </summary>
    public string TraversalMode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the selected leaf index used as the simulated private key.
    /// </summary>
    public int LeafIndex { get; set; }

    /// <summary>
    /// Gets or sets the number of proof items in the authentication path.
    /// </summary>
    public int AuthenticationPathLength { get; set; }

    /// <summary>
    /// Gets or sets the elapsed signature generation time in milliseconds.
    /// </summary>
    public double SignatureGenerationTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the elapsed signature verification time in milliseconds.
    /// </summary>
    public double SignatureVerificationTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the measured memory usage in bytes.
    /// </summary>
    public long MemoryUsageBytes { get; set; }
}
