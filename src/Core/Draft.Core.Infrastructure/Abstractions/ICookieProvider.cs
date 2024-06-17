namespace Draft.Core.Infrastructure.Abstractions;

/// <summary>
/// ICookieProvider
/// </summary>
public interface ICookieProvider
{
    /// <summary>
    /// Set cookie
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expireTime"></param>
    public void Set(string key, string value, int? expireTime = 3600);

    /// <summary>
    /// Get cookie
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string Get(string key);

    /// <summary>
    /// Remove cookie by key
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key);

    /// <summary>
    /// Remove all cookie
    /// </summary>
    public void RemoveAlls();
}
