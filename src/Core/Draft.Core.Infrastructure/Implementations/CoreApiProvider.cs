using Draft.Core.Infrastructure.Abstractions;
using Draft.Infrastructures.Models.Enums;
using Draft.Infrastructures.Models.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections;
using System.Net;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class CoreApiProvider : ICoreApiProvider
{
    /// <summary>
    /// ApiUrl
    /// </summary>
    private readonly string ApiUrl = "";

    /// <summary>
    /// IHttpContextAccessor
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// ILogProvider
    /// </summary>
    private readonly ILogProvider _logProvider;

    /// <summary>
    /// IRestProvider
    /// </summary>
    private readonly IRestProvider _restProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="logProvider"></param>
    /// <param name="restProvider"></param>
    /// <param name="configProvider"></param>
    public CoreApiProvider(
        IHttpContextAccessor httpContextAccessor,
        ILogProvider logProvider,
        IRestProvider restProvider,
        IConfigProvider configProvider)
    {
        ApiUrl = configProvider.GetConfigString("ApiUrl");
        _httpContextAccessor = httpContextAccessor;
        _logProvider = logProvider;
        _restProvider = restProvider;
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> CallCoreApi<T>(
        string url,
        HttpMethod httpMethod,
        object? body = null,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json,
        string keyToken = "",
        bool recall = true,
        bool isExactUrl = false)
    {
        url = !isExactUrl ? ApiUrl + url : url;
        string token = _httpContextAccessor.HttpContext.Request.Cookies[keyToken];
        headers = AddTokenToHeader(headers, token);
        var result = await _restProvider.CallJsonAsync(url, httpMethod, body, queries, headers, contentType, recall);
        return await ConvertResponseToObjectAsync<T>(result);
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> PostCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json,
        string keyToken = "",
        bool recall = true,
        bool isExactUrl = false)
    {
        return await CallCoreApi<T>(url, HttpMethod.Post, body, queries, headers, contentType, keyToken, recall, isExactUrl);
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> GetCore<T>(
        string url,
        object? body = null,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json,
        string keyToken = "",
        bool recall = true,
        bool isExactUrl = false)
    {
        return await CallCoreApi<T>(url, HttpMethod.Get, body, queries, headers, contentType, keyToken, recall, isExactUrl);
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> PutCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json,
        string keyToken = "",
        bool recall = true,
        bool isExactUrl = false)
    {
        return await CallCoreApi<T>(url, HttpMethod.Put, body, queries, headers, contentType, keyToken, recall, isExactUrl);
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> DeleteCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json,
        string keyToken = "",
        bool recall = true,
        bool isExactUrl = false)
    {
        return await CallCoreApi<T>(url, HttpMethod.Delete, body, queries, headers, contentType, keyToken, recall, isExactUrl);
    }

    /// <summary>
    /// ConvertResponseToObjectAsync
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <returns></returns>
    private async Task<ResponseObject<T>> ConvertResponseToObjectAsync<T>(HttpResponseMessage message)
    {
        if (message.StatusCode == HttpStatusCode.OK)
        {
            string responseString = await message.Content.ReadAsStringAsync();
            message.Dispose();
            var result = JsonConvert.DeserializeObject<ResponseObject<T>>(responseString);
            return result;
        }
        else if (message.StatusCode == HttpStatusCode.NotFound)
        {
            return new ResponseObject<T>()
            {
                Code = -1,
                Message = "Không tìm thấy api"
            };
        }
        else if (message.StatusCode == HttpStatusCode.Unauthorized)
        {
            return new ResponseObject<T>()
            {
                Code = 401,
                Message = "Lỗi đăng nhập!"
            };
        }
        else
        {
            var content = await message.Content.ReadAsStringAsync();
            _logProvider.Error("callApi", $"{message?.RequestMessage?.RequestUri} {content}");

            return new ResponseObject<T>()
            {
                Code = -1,
                Message = "Api lỗi"
            };
        }
    }

    /// <summary>
    /// AddTokenToHeader
    /// </summary>
    /// <param name="headers"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private static IDictionary AddTokenToHeader(IDictionary headers, string token)
    {
        headers ??= new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {token}");
        return headers;
    }
}
