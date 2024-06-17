using Draft.Core.Infrastructure.Abstractions;
using Draft.Infrastructures.Models.Enums;
using Draft.Infrastructures.Models.Response;
using System.Collections;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class CustomerApiProvider : ICustomerApiProvider
{
    /// <summary>
    /// KeyToken
    /// </summary>
    private readonly string KeyToken = "";

    /// <summary>
    /// ICoreApiProvider
    /// </summary>
    private readonly ICoreApiProvider _coreApiProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="coreApiProvider"></param>
    /// <param name="configProvider"></param>
    public CustomerApiProvider(ICoreApiProvider coreApiProvider, IConfigProvider configProvider)
    {
        KeyToken = configProvider.GetConfigString("TokenCustomer");
        _coreApiProvider = coreApiProvider;
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> CallCoreApi<T>(
        string url,
        HttpMethod httpMethod,
        object? body = null,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json)
    {
        return await _coreApiProvider.CallCoreApi<T>(url, httpMethod, body, queries, headers, contentType, KeyToken);
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> PostCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType =
        ContentType.Json)
    {
        return await CallCoreApi<T>(url, HttpMethod.Post, body, queries, headers, contentType);
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> GetCore<T>(
        string url,
        object? body = null,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json)
    {
        return await CallCoreApi<T>(url, HttpMethod.Get, body, queries, headers, contentType);
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> PutCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json)
    {
        return await CallCoreApi<T>(url, HttpMethod.Put, body, queries, headers, contentType);
    }

    ///<inheritdoc/>
    public async Task<ResponseObject<T>> DeleteCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json)
    {
        return await CallCoreApi<T>(url, HttpMethod.Delete, body, queries, headers, contentType);
    }
}
