namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Defines the side of a sibling hash inside a Merkle proof.
/// </summary>
public enum ProofItemDirection
{
    /// <summary>
    /// Indicates that the sibling hash belongs to the left side.
    /// </summary>
    Left,

    /// <summary>
    /// Indicates that the sibling hash belongs to the right side.
    /// </summary>
    Right
}

/// <summary>
/// Represents a single item in a Merkle proof authentication path.
/// </summary>
public sealed class ProofItem
{
    /// <summary>
    /// Gets or sets the sibling hash required for proof reconstruction.
    /// </summary>
    public string SiblingHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the side of the sibling hash.
    /// </summary>
    public ProofItemDirection Direction { get; set; }
}
