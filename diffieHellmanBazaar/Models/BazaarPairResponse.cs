using System.Numerics;

namespace diffieHellmanBazaar.Models;

public class BazaarPairResponse
{
    public Guid Id { get; private init; } = Guid.NewGuid();

    public byte[] A { get; init; }
    public byte[] B { get;init; }

    public byte[] N { get; init; }
    public byte[] G { get; init; }

    /// <summary>
    /// Initializes a new instance of the BazaarPairResponse class using the specified pair request and user secret.
    /// Computes the public value B for the cryptographic exchange.
    /// </summary>
    /// <remarks>This constructor performs a modular exponentiation to derive the public value B, which is
    /// used in secure key exchange protocols. Ensure that the provided user secret is securely generated and kept
    /// confidential.</remarks>
    /// <param name="request">The BazaarPairRequest containing the cryptographic parameters A, N, and G required for the exchange.</param>
    /// <param name="userSecret">A byte array representing the user's secret value used to compute the public value B. Must not be null.</param>
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

    /// <summary>
    /// Computes the shared secret for the requester using the provided secret value.
    /// </summary>
    /// <remarks>The shared secret is calculated using modular exponentiation based on the provided secret and
    /// internal parameters. The length and format of the returned byte array may vary depending on the input and
    /// internal values.</remarks>
    /// <param name="userSecretA">A byte array containing the requester's secret value. Must be in big-endian format.</param>
    /// <returns>A byte array representing the computed shared secret. The array is in big-endian format.</returns>
    public byte[] ComputeRequesterSharedSecret(byte[] userSecretA)
    {
        var intB = new BigInteger(B, true);
        var inta = new BigInteger(userSecretA, true);
        var intN = new BigInteger(N, true);

        // Compute s = B^a mod n
        return BigInteger.ModPow(intB, inta, intN).ToByteArray();
    }

    /// <summary>
    /// Computes the responder's shared secret using the provided secret value and internal parameters.
    /// </summary>
    /// <remarks>This method performs modular exponentiation using internal parameters. The caller is
    /// responsible for ensuring that the input is valid and compatible with the expected cryptographic
    /// protocol.</remarks>
    /// <param name="userSecretB">The secret value from the responder, represented as a byte array. Must be in big-endian format.</param>
    /// <returns>A byte array containing the computed shared secret. The array is in big-endian format.</returns>
    public byte[] ComputeResponderSharedSecret(byte[] userSecretB)
    {
        var intA = new BigInteger(A, true);
        var intb = new BigInteger(userSecretB, true);
        var intN = new BigInteger(N, true);

        // Compute s = A^b mod n
        return BigInteger.ModPow(intA, intb, intN).ToByteArray();
    }
}
