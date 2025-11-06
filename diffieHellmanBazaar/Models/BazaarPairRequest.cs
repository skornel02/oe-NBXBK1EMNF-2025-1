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
