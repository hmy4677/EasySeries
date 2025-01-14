using System.Security.Cryptography;

namespace EasySeries.MiniProgram.Utils;

internal class AES
{
    internal static string Decrypt(string encryptedData, string sessionKey, string iv)
    {
        var encryptedBytes = Convert.FromBase64String(encryptedData);
        var sessionKeyBytes = Convert.FromBase64String(sessionKey);
        var ivBytes = Convert.FromBase64String(iv);

        using Aes aes = Aes.Create();
        aes.IV = ivBytes;
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;

        using ICryptoTransform transform = aes.CreateDecryptor(sessionKeyBytes, aes.IV);
        using MemoryStream stream = new MemoryStream(encryptedBytes);
        using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
        using StreamReader streamReader = new StreamReader(stream2);
        return streamReader.ReadToEnd();
    }
}
