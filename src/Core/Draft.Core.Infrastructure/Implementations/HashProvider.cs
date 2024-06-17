using Draft.Core.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class HashProvider : IHashProvider
{
    ///<inheritdoc/>
    public string HashPassword(string password)
    {
        return new PasswordHasher<object>().HashPassword(null, password);
    }

    ///<inheritdoc/>
    public bool VerifyPassword(string hashedPassword, string password)
    {
        return new PasswordHasher<object>().VerifyHashedPassword(null, hashedPassword, password) != PasswordVerificationResult.Failed;
    }

    ///<inheritdoc/>
    public string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);

        // Convert the byte array to hexadecimal string
        var sb = new StringBuilder();

        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }

        return sb.ToString().ToLower();
    }

    ///<inheritdoc/>
    public string SHA256Encrypt(string input, string secret)
    {
        var encoding = new ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(input);

        using var hmacsha256 = new HMACSHA256(keyByte);
        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
        return Convert.ToBase64String(hashmessage);
    }

    ///<inheritdoc/>
    public string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    ///<inheritdoc/>
    public string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
