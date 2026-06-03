namespace MerkleCloudIntegrity.Services;

/// <summary>
/// Provides hashing operations for Merkle Tree nodes and data blocks.
/// </summary>
public sealed class HashService
{
    /// <summary>
    /// Computes a SHA-256 hash for the supplied byte array.
    /// </summary>
    /// <param name="data">The input data to hash.</param>
    /// <returns>The hexadecimal SHA-256 hash value.</returns>
    public string ComputeHash(byte[] data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Computes a parent hash from two child node hashes.
    /// </summary>
    /// <param name="leftHash">The left child hash.</param>
    /// <param name="rightHash">The right child hash.</param>
    /// <returns>The hexadecimal SHA-256 parent hash.</returns>
    public string ComputeCombinedHash(string leftHash, string rightHash)
    {
        throw new NotImplementedException();
    }
}
