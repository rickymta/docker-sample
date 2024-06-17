namespace Draft.Core.Infrastructure.Abstractions;

/// <summary>
/// IConfigProvider
/// </summary>
public interface IConfigProvider
{
    /// <summary>
    /// GetConfig
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object? GetConfig(string key);

    /// <summary>
    /// GetConfigString
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string? GetConfigString(string key);

    /// <summary>
    /// SaveConfig
    /// </summary>
    /// <param name="data"></param>
    public void SaveConfig(object data);
}
