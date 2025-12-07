using System.Diagnostics.CodeAnalysis;

namespace diffieHellmanBazaar.Models;

/// <summary>
/// This is the atom of the software. An open communication message that can contain text, files, or pairing requests/responses.
/// 
/// This class is an abstract representation of what a message could be in an open communication system. 
/// 
/// This abstraction contains additional properties for better tracing, but in real life SenderId and EncryptionKeyId would not be present in the message itself.
/// These are only here to allow the program to illustrate better how an attacker could try to intercept and read messages not meant for them, but revealing this.
/// </summary>
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
    public Guid? FileId { get; set; }

    [MemberNotNullWhen(true, nameof(Filename), nameof(FileId))]
    public bool HasFile { get; set; }
}
