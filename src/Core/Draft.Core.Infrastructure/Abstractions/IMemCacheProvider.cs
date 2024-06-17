namespace Draft.Core.Infrastructure.Abstractions;

/// <summary>
/// ICacheProvider
/// </summary>
public interface IMemCacheProvider
{
    /// <summary>
    /// Get cache
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object Get(string key);

    /// <summary>
    /// Set cache
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public object Set(string key, object value);

    /// <summary>
    /// Set cache with expiration time
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="absoluteExpiration"></param>
    /// <returns></returns>
    public object Set(string key, object value, DateTimeOffset absoluteExpiration);

    /// <summary>
    /// Delete cache by key
    /// </summary>
    /// <param name="key"></param>
    public void Delete(string key);
}
