namespace diffieHellmanBazaar.Models;

public class BazaarMessage
{
    public Guid Id { get; private init; } = Guid.NewGuid();

    public required string Sender { get; set; }
    public required Guid SenderId { get; set; }

    public Guid? EncryptionKeyId { get; set; }

    public string? Message { get; set; }

    public BazaarPairRequest? PairRequest { get; set; }

    public BazaarPairResponse? PairResponse { get; set; }

    public string? Filename { get; set; }
}
