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
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (blockSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(blockSize), "Block size must be greater than zero.");
        }

        using var fileStream = File.OpenRead(filePath);
        var blocks = new List<DataBlock>();
        var buffer = new byte[blockSize];
        int bytesRead;

        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            var content = new byte[bytesRead];
            Array.Copy(buffer, content, bytesRead);

            blocks.Add(new DataBlock
            {
                Index = blocks.Count,
                Content = content,
                Hash = _hashService.ComputeHash(content)
            });
        }

        return blocks;
    }

    /// <summary>
    /// Builds a Merkle Tree from the supplied data blocks.
    /// </summary>
    /// <param name="blocks">The input data blocks.</param>
    /// <returns>The root node of the constructed tree.</returns>
    public MerkleNode BuildTree(IReadOnlyList<DataBlock> blocks)
    {
        ArgumentNullException.ThrowIfNull(blocks);

        if (blocks.Count == 0)
        {
            throw new ArgumentException("At least one data block is required.", nameof(blocks));
        }

        var currentLevel = blocks
            .Select(CreateLeafNode)
            .ToList();

        while (currentLevel.Count > 1)
        {
            currentLevel = BuildParentLevel(currentLevel);
        }

        return currentLevel[0];
    }

    /// <summary>
    /// Gets the Merkle Root hash from a constructed tree.
    /// </summary>
    /// <param name="root">The root node of the tree.</param>
    /// <returns>The Merkle Root hash.</returns>
    public string GetMerkleRoot(MerkleNode root)
    {
        ArgumentNullException.ThrowIfNull(root);

        return root.Hash;
    }

    /// <summary>
    /// Generates a Merkle proof for the specified data block index.
    /// </summary>
    /// <param name="root">The root node of the tree.</param>
    /// <param name="blockIndex">The index of the target data block.</param>
    /// <returns>A collection of proof items.</returns>
    public IReadOnlyList<ProofItem> GenerateProof(MerkleNode root, int blockIndex)
    {
        ArgumentNullException.ThrowIfNull(root);

        var leaf = FindLeaf(root, blockIndex)
            ?? throw new ArgumentException($"Block index {blockIndex} was not found in the Merkle Tree.", nameof(blockIndex));

        var proof = new List<ProofItem>();
        var current = leaf;

        while (current.Parent is not null)
        {
            var parent = current.Parent;
            var isLeftChild = ReferenceEquals(parent.Left, current);
            var sibling = isLeftChild ? parent.Right : parent.Left;

            if (sibling is not null)
            {
                proof.Add(new ProofItem
                {
                    SiblingHash = sibling.Hash,
                    Direction = isLeftChild ? ProofItemDirection.Right : ProofItemDirection.Left
                });
            }

            current = parent;
        }

        return proof;
    }

    private MerkleNode CreateLeafNode(DataBlock block)
    {
        if (block.Content.Length == 0)
        {
            throw new ArgumentException("Data blocks must contain at least one byte.", nameof(block));
        }

        block.Hash = _hashService.ComputeHash(block.Content);

        return new MerkleNode
        {
            Hash = block.Hash,
            BlockIndex = block.Index
        };
    }

    private List<MerkleNode> BuildParentLevel(IReadOnlyList<MerkleNode> currentLevel)
    {
        var parentLevel = new List<MerkleNode>();

        for (var index = 0; index < currentLevel.Count; index += 2)
        {
            var left = currentLevel[index];
            var right = index + 1 < currentLevel.Count
                ? currentLevel[index + 1]
                : DuplicateNode(left);

            var parent = new MerkleNode
            {
                Left = left,
                Right = right,
                Hash = _hashService.ComputeCombinedHash(left.Hash, right.Hash)
            };

            left.Parent = parent;
            right.Parent = parent;
            parentLevel.Add(parent);
        }

        return parentLevel;
    }

    private static MerkleNode DuplicateNode(MerkleNode node)
    {
        return new MerkleNode
        {
            Hash = node.Hash,
            BlockIndex = node.BlockIndex
        };
    }

    private static MerkleNode? FindLeaf(MerkleNode node, int blockIndex)
    {
        if (node.IsLeaf)
        {
            return node.BlockIndex == blockIndex ? node : null;
        }

        return node.Left is not null && FindLeaf(node.Left, blockIndex) is { } leftResult
            ? leftResult
            : node.Right is not null
                ? FindLeaf(node.Right, blockIndex)
                : null;
    }
}
