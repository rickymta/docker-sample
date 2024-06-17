namespace Draft.Core.Infrastructure.Abstractions;

/// <summary>
/// ILogProvider
/// </summary>
public interface ILogProvider
{
    /// <summary>
    /// Custom
    /// </summary>
    /// <param name="key"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="fileName"></param>
    public void Custom(string key, string message, object data, string? fileName = null);

    /// <summary>
    /// Custom
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <param name="json"></param>
    /// <param name="fileName"></param>
    public void Custom(string key, object data, bool json = false, string? fileName = null);

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <param name="json"></param>
    public void Error(string key, object data, bool json = false);

    /// <summary>
    /// Error
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="message"></param>
    public void Error(Exception ex);

    /// <summary>
    /// ErrorWithKey
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ex"></param>
    public void ErrorWithKey(string key, Exception ex);
}
