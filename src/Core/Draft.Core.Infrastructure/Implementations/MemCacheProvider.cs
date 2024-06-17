using Draft.Core.Infrastructure.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class MemCacheProvider : IMemCacheProvider
{
    /// <summary>
    /// IMemoryCache
    /// </summary>
    private readonly IMemoryCache _cache;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="memoryCache"></param>
    public MemCacheProvider(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    ///<inheritdoc/>
    public object Get(string key)
    {
        _cache.TryGetValue(key, out var result);
        return result ?? false;
    }

    ///<inheritdoc/>
    public object Set(string key, object value)
    {
        return _cache.Set(key, value);
    }

    ///<inheritdoc/>
    public object Set(string key, object value, DateTimeOffset absoluteExpiration)
    {
        return _cache.Set(key, value, absoluteExpiration);
    }

    ///<inheritdoc/>
    public void Delete(string key)
    {
        _cache.Remove(key);
    }
}
