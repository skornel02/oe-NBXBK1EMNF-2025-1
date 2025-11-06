namespace diffieHellmanBazaar.Models;

public class BazaarSecretExchange
{
    public required string FriendName { get; init; }

    public required int SequenceNumber { get; init; }

    public required byte[] CommonSecret { get; init; }
}
