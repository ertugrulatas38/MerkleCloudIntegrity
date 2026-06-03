namespace MerkleCloudIntegrity.Models;

/// <summary>
/// Represents a simulated Merkle-based signature.
/// </summary>
public sealed class MerkleSignature
{
    /// <summary>
    /// Gets or sets the leaf index used as the simulated private key index.
    /// </summary>
    public int LeafIndex { get; set; }

    /// <summary>
    /// Gets or sets the signed leaf block.
    /// </summary>
    public DataBlock LeafBlock { get; set; } = new();

    /// <summary>
    /// Gets or sets the authentication path used to verify the signature.
    /// </summary>
    public IReadOnlyList<ProofItem> AuthenticationPath { get; set; } = Array.Empty<ProofItem>();
}
