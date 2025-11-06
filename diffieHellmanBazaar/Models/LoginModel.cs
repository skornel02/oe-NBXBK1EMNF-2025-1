using System.Security.Cryptography;

namespace diffieHellmanBazaar.Models;

public class LoginModel
{
    private const int SecretSize = 16;

    public Guid Id { get; private init; } = Guid.NewGuid();
    public required string Username { get; init; }
    public byte[] Secret { get; init; } = new byte[SecretSize];

    public LoginModel()
    {
        RandomNumberGenerator.Fill(Secret);
    }

    public string IdString => Convert.ToBase64String(Id.ToByteArray());

    public string SecretText => Convert.ToBase64String(Secret);
}
