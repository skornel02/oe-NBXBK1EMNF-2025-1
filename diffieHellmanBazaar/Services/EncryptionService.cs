using System.Security.Cryptography;
using System.Text;

namespace diffieHellmanBazaar.Services;

/// <summary>
/// Provides methods for encrypting and decrypting messages using symmetric encryption with a shared secret.
/// </summary>
/// <remarks>This service uses AES symmetric encryption and requires a shared secret for both encryption and
/// decryption. The encrypted message format includes the initialization vector (IV) and ciphertext, separated by an
/// exclamation mark. Logging is performed for both successful and failed operations.</remarks>
/// <param name="_logger">The logger used to record informational and error messages during encryption and decryption operations.</param>
public class EncryptionService(ILogger<EncryptionService> _logger)
{
    public string EncryptMessage(string message, byte[] commonSecret)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(message);

        using SymmetricAlgorithm crypt = Aes.Create();
        using HashAlgorithm hash = MD5.Create();
        using MemoryStream memoryStream = new();
        crypt.Padding = PaddingMode.PKCS7;
        crypt.Key = hash.ComputeHash(commonSecret);
        // This is really only needed before you call CreateEncryptor the second time,
        // since it starts out random.  But it's here just to show it exists.
        crypt.GenerateIV();

        using var cryptoStream = new CryptoStream(
            memoryStream,
            crypt.CreateEncryptor(),
            CryptoStreamMode.Write);

        cryptoStream.Write(bytes, 0, bytes.Length);
        cryptoStream.FlushFinalBlock();

        string base64IV = Convert.ToBase64String(crypt.IV);
        string base64Ciphertext = Convert.ToBase64String(memoryStream.ToArray());

        var encrypted = $"{base64IV}!{base64Ciphertext}";

        _logger.LogInformation("Encrypted message: '{Message}' => '{EncryptedMessage}'", message, encrypted);

        return encrypted;
    }

    public string DecryptMessage(string encryptedMessage, byte[] commonSecret, ref bool failed)
    {
        failed = false;
        try
        {
            var parts = encryptedMessage.Split('!');
            var iv = Convert.FromBase64String(parts[0]);
            var ciphertext = Convert.FromBase64String(parts[1]);

            using var crypt = Aes.Create();
            using var hash = MD5.Create();
            using var memoryStream = new MemoryStream();
            crypt.Padding = PaddingMode.PKCS7;
            crypt.Key = hash.ComputeHash(commonSecret);
            crypt.IV = iv;

            using var cryptoStream = new CryptoStream(
                memoryStream,
                crypt.CreateDecryptor(),
                CryptoStreamMode.Write);

            cryptoStream.Write(ciphertext, 0, ciphertext.Length);
            cryptoStream.FlushFinalBlock();

            var message = Encoding.UTF8.GetString(memoryStream.ToArray());

            _logger.LogInformation("Decrypted message: '{EncryptedMessage}' => '{Message}'", encryptedMessage, message);

            return message;
        }
        catch (Exception ex)
        {
            failed = true;
            _logger.LogError(ex, "Failed to decrypt message: '{EncryptedMessage}'", encryptedMessage);
            return encryptedMessage;
        }
    }
}
