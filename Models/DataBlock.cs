namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents one indexed file segment that is hashed into a Merkle leaf.
/// </summary>
public sealed class DataBlock
{
    /// <summary>
    /// Gets or sets the zero-based position of the segment in the original file.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the segment bytes used during hashing and verification.
    /// </summary>
    public byte[] Content { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Gets or sets the cached SHA-256 digest for the segment.
    /// </summary>
    public string Hash { get; set; } = string.Empty;
}
