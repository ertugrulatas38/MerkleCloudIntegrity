namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents the minimal data published during the Merkle signature experiment.
/// </summary>
public sealed class MerkleSignature
{
    /// <summary>
    /// Gets or sets the selected one-time key position in the leaf set.
    /// </summary>
    public int LeafIndex { get; set; }

    /// <summary>
    /// Gets or sets the revealed leaf value for this simulated signing operation.
    /// </summary>
    public DataBlock LeafBlock { get; set; } = new();

    /// <summary>
    /// Gets or sets the sibling hashes needed to reconstruct the public Merkle root.
    /// </summary>
    public IReadOnlyList<ProofItem> AuthenticationPath { get; set; } = Array.Empty<ProofItem>();
}
