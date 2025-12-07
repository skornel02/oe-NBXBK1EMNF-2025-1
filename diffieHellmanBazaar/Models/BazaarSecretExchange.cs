namespace diffieHellmanBazaar.Models;

public class BazaarSecretExchange
{
    /// <summary>
    /// Gets the name of the friend associated with this instance.
    /// </summary>
    public required string FriendName { get; init; }

    /// <summary>
    /// Gets the sequence number associated with this instance.
    /// </summary>
    public required int SequenceNumber { get; init; }

    /// <summary>
    /// Gets the shared secret used for cryptographic operations between parties.
    /// </summary>
    /// <remarks>
    /// The array should contain the agreed-upon secret bytes for secure communication or key derivation. 
    /// The caller is responsible for managing the lifetime and security of the secret.
    /// </remarks>
    public required byte[] CommonSecret { get; init; }
}
