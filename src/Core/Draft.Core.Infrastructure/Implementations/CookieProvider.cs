using Draft.Core.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class CookieProvider : ICookieProvider
{
    /// <summary>
    /// IHttpContextAccessor
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public CookieProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    ///<inheritdoc/>
    public void Set(string key, string value, int? expireTime = 3600)
    {
        expireTime ??= 3600;

        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(expireTime.Value)
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
    }

    ///<inheritdoc/>
    public string Get(string key)
    {
        return _httpContextAccessor.HttpContext.Request.Cookies[key];
    }

    ///<inheritdoc/>
    public void Remove(string key)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
    }

    ///<inheritdoc/>
    public void RemoveAlls()
    {
        foreach (string key in _httpContextAccessor.HttpContext.Request.Cookies.Keys)
        {
            Remove(key);
        }
    }
}
