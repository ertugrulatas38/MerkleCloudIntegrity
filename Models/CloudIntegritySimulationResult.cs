namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents the outcome of a file block integrity simulation.
/// </summary>
public sealed class CloudIntegritySimulationResult
{
    /// <summary>
    /// Gets or sets the number of blocks generated from the source file.
    /// </summary>
    public int FileBlockCount { get; set; }

    /// <summary>
    /// Gets or sets the original Merkle Root generated before tampering.
    /// </summary>
    public string OriginalMerkleRoot { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the block index selected for proof verification.
    /// </summary>
    public int TargetBlockIndex { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the original block verification succeeded.
    /// </summary>
    public bool OriginalVerificationResult { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the tampered block verification succeeded.
    /// </summary>
    public bool TamperedVerificationResult { get; set; }
}
