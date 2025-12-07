using System.Security.Cryptography;

namespace diffieHellmanBazaar.Models;

public class LoginModel
{
    private const int SecretSize = 16;

    public Guid Id { get; private init; } = Guid.NewGuid();
    public required string Username { get; init; }
    public byte[] Secret { get; init; } = new byte[SecretSize];

    /// <summary>
    /// Initializes a new instance of the LoginModel class and generates a random secret value for authentication
    /// purposes.
    /// </summary>
    /// <remarks>The generated secret is intended to enhance security by providing a unique value for each
    /// instance. This constructor should be used when a fresh authentication context is required.</remarks>
    public LoginModel()
    {
        RandomNumberGenerator.Fill(Secret);
    }

    public string IdString => Convert.ToBase64String(Id.ToByteArray());

    public string SecretText => Convert.ToBase64String(Secret);
}
