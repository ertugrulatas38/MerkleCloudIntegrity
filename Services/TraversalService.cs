using MerkleCloudIntegrity.Models;

namespace MerkleCloudIntegrity.Services;

/// <summary>
/// Provides classical and cached Merkle authentication path traversal operations.
/// </summary>
public sealed class TraversalService
{
    private readonly MerkleTreeService _merkleTreeService;
    private readonly Dictionary<string, IReadOnlyList<ProofItem>> _proofCache = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="TraversalService"/> class.
    /// </summary>
    /// <param name="merkleTreeService">The Merkle Tree service used for proof construction.</param>
    public TraversalService(MerkleTreeService merkleTreeService)
    {
        _merkleTreeService = merkleTreeService;
    }

    /// <summary>
    /// Generates a proof by traversing the tree from scratch.
    /// </summary>
    /// <param name="root">The Merkle Tree root node.</param>
    /// <param name="blockIndex">The target block index.</param>
    /// <returns>The generated authentication path.</returns>
    public IReadOnlyList<ProofItem> GenerateClassicalProof(MerkleNode root, int blockIndex)
    {
        return _merkleTreeService.GenerateProof(root, blockIndex);
    }

    /// <summary>
    /// Generates or retrieves a cached proof for the target block.
    /// </summary>
    /// <param name="root">The Merkle Tree root node.</param>
    /// <param name="blockIndex">The target block index.</param>
    /// <returns>The cached or newly generated authentication path.</returns>
    public IReadOnlyList<ProofItem> GenerateCachedProof(MerkleNode root, int blockIndex)
    {
        ArgumentNullException.ThrowIfNull(root);

        var cacheKey = CreateCacheKey(root.Hash, blockIndex);
        if (_proofCache.TryGetValue(cacheKey, out var cachedProof))
        {
            return cachedProof;
        }

        var proof = _merkleTreeService.GenerateProof(root, blockIndex);
        _proofCache[cacheKey] = proof;
        return proof;
    }

    /// <summary>
    /// Clears all cached authentication paths.
    /// </summary>
    public void ClearCache()
    {
        _proofCache.Clear();
    }

    private static string CreateCacheKey(string rootHash, int blockIndex)
    {
        return $"{rootHash}:{blockIndex}";
    }
}
