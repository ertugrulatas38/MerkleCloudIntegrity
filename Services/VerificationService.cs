using MerkleCloudIntegrity.Models;

namespace MerkleCloudIntegrity.Services;

/// <summary>
/// Provides verification operations for Merkle proofs and root hashes.
/// </summary>
public sealed class VerificationService
{
    private readonly HashService _hashService;

    /// <summary>
    /// Initializes a new instance of the <see cref="VerificationService"/> class.
    /// </summary>
    /// <param name="hashService">The hash service used during verification.</param>
    public VerificationService(HashService hashService)
    {
        _hashService = hashService;
    }

    /// <summary>
    /// Verifies that a data block belongs to a tree with the expected Merkle Root.
    /// </summary>
    /// <param name="block">The data block to verify.</param>
    /// <param name="proof">The Merkle proof items.</param>
    /// <param name="expectedRootHash">The expected root hash.</param>
    /// <returns>True when the proof is valid; otherwise, false.</returns>
    public bool VerifyProof(DataBlock block, IReadOnlyList<ProofItem> proof, string expectedRootHash)
    {
        throw new NotImplementedException();
    }
}
