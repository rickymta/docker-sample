using Draft.Infrastructures.Models.Enums;
using Draft.Infrastructures.Models.Response;
using System.Collections;

namespace Draft.Core.Infrastructure.Abstractions;

/// <summary>
/// ICustomerApiProvider
/// </summary>
public interface ICustomerApiProvider
{
    /// <summary>
    /// CallCoreApi
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="httpMethod"></param>
    /// <param name="body"></param>
    /// <param name="queries"></param>
    /// <param name="headers"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public Task<ResponseObject<T>> CallCoreApi<T>(
        string url,
        HttpMethod httpMethod,
        object? body = null,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json);

    /// <summary>
    /// PostCore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="queries"></param>
    /// <param name="headers"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public Task<ResponseObject<T>> PostCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType =
        ContentType.Json);

    /// <summary>
    /// GetCore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="queries"></param>
    /// <param name="headers"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public Task<ResponseObject<T>> GetCore<T>(
        string url,
        object? body = null,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json);

    /// <summary>
    /// PutCore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="queries"></param>
    /// <param name="headers"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public Task<ResponseObject<T>> PutCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json);

    /// <summary>
    /// DeleteCore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="body"></param>
    /// <param name="queries"></param>
    /// <param name="headers"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public Task<ResponseObject<T>> DeleteCore<T>(
        string url,
        object body,
        object? queries = null,
        IDictionary? headers = null,
        ContentType contentType = ContentType.Json);
}
