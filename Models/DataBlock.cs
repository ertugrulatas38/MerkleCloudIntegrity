namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents a fixed-size piece of cloud data used as a Merkle tree leaf.
/// </summary>
public sealed class DataBlock
{
    /// <summary>
    /// Gets or sets the zero-based position of the block in the source data.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the raw byte content of the block.
    /// </summary>
    public byte[] Content { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Gets or sets the SHA-256 hash assigned to the block.
    /// </summary>
    public string Hash { get; set; } = string.Empty;
}
