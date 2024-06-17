namespace Draft.Core.Infrastructure.Abstractions;

/// <summary>
/// IStringProvider
/// </summary>
public interface IStringProvider
{
    /// <summary>
    /// FormatNumber
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public string FormatNumber(object number);

    /// <summary>
    /// GenerateOTP
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public string GenerateOTP(int length = 6);

    /// <summary>
    /// SerializeUtf8
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public string SerializeUtf8<T>(T data);

    /// <summary>
    /// CheckNumberic
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public bool CheckNumberic(string text);

    /// <summary>
    /// FormatPhoneNumber
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public string FormatPhoneNumber(string phoneNumber);

    /// <summary>
    /// RandomString
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public string RandomString(int length);

    /// <summary>
    /// CheckNomalText
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public bool CheckNomalText(string text);

    /// <summary>
    /// GetNumberFromText
    /// </summary>
    /// <param name="text"></param>
    /// <param name="preText"></param>
    /// <param name="nextText"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    public long? GetNumberFromText(string text, string preText, string nextText, List<string>? replace = null);

    /// <summary>
    /// ReplaceFirst
    /// </summary>
    /// <param name="text"></param>
    /// <param name="search"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    public string ReplaceFirst(string text, string search, string replace);

    /// <summary>
    /// IsValidEmail
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public bool IsValidEmail(string email);

    /// <summary>
    /// UnixTimeStampToDateTime
    /// </summary>
    /// <param name="unixTimeStamp"></param>
    /// <returns></returns>
    public DateTime UnixTimeStampToDateTime(double unixTimeStamp);

    /// <summary>
    /// GetTimestamp
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string GetTimestamp(DateTime value);
}
