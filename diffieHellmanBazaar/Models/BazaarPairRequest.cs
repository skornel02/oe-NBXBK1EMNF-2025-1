using System.Numerics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection;

namespace diffieHellmanBazaar.Models;

public class BazaarPairRequest
{
    private const int SecretSize = 4;

    public Guid Id {get; private init; } = Guid.NewGuid();

    public byte[] A { get; init; } = new byte[SecretSize];

    public byte[] N { get; init; } = new byte[SecretSize];
    public byte[] G { get; init; } = new byte[SecretSize];

    /// <summary>
    /// Initializes a new instance of the BazaarPairRequest class using the specified user secret to generate
    /// cryptographic parameters for a key exchange.
    /// </summary>
    /// <remarks>This constructor generates random values for the cryptographic parameters and computes the
    /// public value required for the key exchange protocol. The provided user secret is used as part of the calculation
    /// to ensure uniqueness and security of the generated parameters.</remarks>
    /// <param name="userSecret">A byte array containing the user's secret value used in the key exchange computation. Must not be null or empty.</param>
    public BazaarPairRequest(byte[] userSecret)
    {
        RandomNumberGenerator.Fill(N);
        RandomNumberGenerator.Fill(G);

        var intG = new BigInteger(G, true);
        var inta = new BigInteger(userSecret, true);
        var intN = new BigInteger(N, true);
        // Compute A = g^a mod n
        A = BigInteger.ModPow(intG, inta, intN).ToByteArray();
    }
}
