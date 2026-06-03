namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents a node in the Merkle Tree structure.
/// </summary>
public sealed class MerkleNode
{
    /// <summary>
    /// Gets or sets the SHA-256 hash value stored by the node.
    /// </summary>
    public string Hash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the left child node.
    /// </summary>
    public MerkleNode? Left { get; set; }

    /// <summary>
    /// Gets or sets the right child node.
    /// </summary>
    public MerkleNode? Right { get; set; }

    /// <summary>
    /// Gets or sets the source block index when the node represents a leaf.
    /// </summary>
    public int? BlockIndex { get; set; }

    /// <summary>
    /// Gets a value indicating whether the node is a leaf.
    /// </summary>
    public bool IsLeaf => Left is null && Right is null;
}
