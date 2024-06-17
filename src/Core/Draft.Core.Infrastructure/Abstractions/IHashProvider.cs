namespace Draft.Core.Infrastructure.Abstractions;

/// <summary>
/// IHashProvider
/// </summary>
public interface IHashProvider
{
    /// <summary>
    /// HashPassword
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string HashPassword(string password);

    /// <summary>
    /// VerifyPassword
    /// </summary>
    /// <param name="hashedPassword"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool VerifyPassword(string hashedPassword, string password);

    /// <summary>
    /// CreateMD5
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public string CreateMD5(string input);

    /// <summary>
    /// SHA256Encrypt
    /// </summary>
    /// <param name="input"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    public string SHA256Encrypt(string input, string secret);

    /// <summary>
    /// Base64Encode
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public string Base64Encode(string plainText);

    /// <summary>
    /// Base64Decode
    /// </summary>
    /// <param name="base64EncodedData"></param>
    /// <returns></returns>
    public string Base64Decode(string base64EncodedData);
}
