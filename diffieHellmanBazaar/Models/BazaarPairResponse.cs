using System.Numerics;

namespace diffieHellmanBazaar.Models;

public class BazaarPairResponse
{
    public Guid Id { get; private init; } = Guid.NewGuid();

    public byte[] A { get; init; }
    public byte[] B { get;init; }

    public byte[] N { get; init; }
    public byte[] G { get; init; }

    public BazaarPairResponse(BazaarPairRequest request, byte[] userSecret)
    {
        A = request.A;
        N = request.N;
        G = request.G;

        var intG = new BigInteger(G, true);
        var intb = new BigInteger(userSecret, true);
        var intN = new BigInteger(N, true);
        // Compute B = g^b mod n
        B = BigInteger.ModPow(intG, intb, intN).ToByteArray();
    }

    public byte[] ComputeRequesterSharedSecret(byte[] userSecretA)
    {
        var intB = new BigInteger(B, true);
        var inta = new BigInteger(userSecretA, true);
        var intN = new BigInteger(N, true);

        // Compute s = B^a mod n
        return BigInteger.ModPow(intB, inta, intN).ToByteArray();
    }

    public byte[] ComputeResponderSharedSecret(byte[] userSecretB)
    {
        var intA = new BigInteger(A, true);
        var intb = new BigInteger(userSecretB, true);
        var intN = new BigInteger(N, true);

        // Compute s = A^b mod n
        return BigInteger.ModPow(intA, intb, intN).ToByteArray();
    }
}
