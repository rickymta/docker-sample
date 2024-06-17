using Draft.Infrastructures.Models.Enums;
using System.Collections;

namespace Draft.Core.Infrastructure.Abstractions;

/// <summary>
/// IRestProvider
/// </summary>
public interface IRestProvider
{
    /// <summary>
    /// CallJsonAsync
    /// </summary>
    /// <param name="url"></param>
    /// <param name="httpMethod"></param>
    /// <param name="body"></param>
    /// <param name="queries"></param>
    /// <param name="headers"></param>
    /// <param name="contentType"></param>
    /// <param name="recall"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public Task<HttpResponseMessage> CallJsonAsync(
        string url,
        HttpMethod httpMethod,
        object? body = null,
        object? queries = null,
        IDictionary? headers = null,
        ContentType? contentType = ContentType.Json,
        bool? recall = true,
        int? timeout = 600);

    /// <summary>
    /// ConvertObjectToList
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public List<string> ConvertObjectToList(object data);

    /// <summary>
    /// ConvertObjectToKeyValuePair
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IList<KeyValuePair<string, string>> ConvertObjectToKeyValuePair(dynamic obj);

    /// <summary>
    /// ConvertHttpResponseMessageToObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <returns></returns>
    public Task<T> ConvertHttpResponseMessageToObject<T>(HttpResponseMessage response);
}
