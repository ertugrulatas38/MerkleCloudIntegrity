using MerkleCloudIntegrity.Models;

namespace MerkleCloudIntegrity.Services;

/// <summary>
/// Coordinates Merkle Tree construction, root generation, and proof generation.
/// </summary>
public sealed class MerkleTreeService
{
    private readonly HashService _hashService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MerkleTreeService"/> class.
    /// </summary>
    /// <param name="hashService">The hash service used by the tree service.</param>
    public MerkleTreeService(HashService hashService)
    {
        _hashService = hashService;
    }

    /// <summary>
    /// Splits a file into fixed-size data blocks.
    /// </summary>
    /// <param name="filePath">The path of the source file.</param>
    /// <param name="blockSize">The target block size in bytes.</param>
    /// <returns>A collection of data blocks.</returns>
    public IReadOnlyList<DataBlock> SplitFile(string filePath, int blockSize)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Builds a Merkle Tree from the supplied data blocks.
    /// </summary>
    /// <param name="blocks">The input data blocks.</param>
    /// <returns>The root node of the constructed tree.</returns>
    public MerkleNode BuildTree(IReadOnlyList<DataBlock> blocks)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the Merkle Root hash from a constructed tree.
    /// </summary>
    /// <param name="root">The root node of the tree.</param>
    /// <returns>The Merkle Root hash.</returns>
    public string GetMerkleRoot(MerkleNode root)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Generates a Merkle proof for the specified data block index.
    /// </summary>
    /// <param name="root">The root node of the tree.</param>
    /// <param name="blockIndex">The index of the target data block.</param>
    /// <returns>A collection of proof items.</returns>
    public IReadOnlyList<ProofItem> GenerateProof(MerkleNode root, int blockIndex)
    {
        throw new NotImplementedException();
    }
}
