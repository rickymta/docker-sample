using Draft.Core.Infrastructure.Abstractions;
using Draft.Infrastructures.Models.Enums;
using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class RestProvider : IRestProvider
{
    /// <summary>
    /// HttpClientHandler
    /// </summary>
    private static readonly HttpClientHandler handler = new() { UseCookies = false, AllowAutoRedirect = false };

    /// <summary>
    /// HttpClient
    /// </summary>
    private static readonly HttpClient Client = new(handler);

    /// <summary>
    /// runFirstFlag
    /// </summary>
    private static bool runFirstFlag = true;

    /// <summary>
    /// ILogProvider
    /// </summary>
    private readonly ILogProvider _logProvider;

    /// <summary>
    /// IStringProvider
    /// </summary>
    private readonly IStringProvider _stringProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logProvider"></param>
    /// <param name="stringProvider"></param>
    public RestProvider(ILogProvider logProvider, IStringProvider stringProvider)
    {
        _logProvider = logProvider;
        _stringProvider = stringProvider;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> CallJsonAsync(string url, HttpMethod httpMethod, object? body = null, object? queries = null, IDictionary? headers = null, ContentType? contentType = ContentType.Json, bool? recall = true, int? timeout = 600)
    {
        if (runFirstFlag)
        {
            Client.Timeout = TimeSpan.FromSeconds(600);
            runFirstFlag = false;
        }
        int count = 0;
        bool continueCall = true;
        // thử gọi lại nếu có lỗi
        HttpResponseMessage result = new HttpResponseMessage();

        while (continueCall)
        {
            try
            {
                using var httpRequestMessage = new HttpRequestMessage();
                var contentTypeHeader = "application/json";

                if (contentType != ContentType.Json)
                {
                    if (contentType == ContentType.Form)
                    {
                        contentTypeHeader = "application/x-www-form-urlencoded";

                        if (body != null)
                        {
                            var nameValueCollection = ConvertObjectToKeyValuePair(body);
                            httpRequestMessage.Content = new FormUrlEncodedContent(nameValueCollection);
                        }
                    }
                }
                else
                {
                    if (body != null)
                    {
                        httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, contentTypeHeader);
                    }
                }

                if (queries != null)
                {
                    List<string> strArr = ConvertObjectToList(queries);
                    string firstChar = url.Contains("?") ? "&" : "?";
                    if (strArr.Count > 0)
                    {
                        url += firstChar + string.Join("&", strArr);
                    }
                }

                // add content-type and accept application/json hoặc application/x-www-form-urlencoded
                httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentTypeHeader));
                httpRequestMessage.Method = httpMethod;
                httpRequestMessage.RequestUri = new Uri(url);

                if (headers != null && headers.Count > 0)
                {
                    foreach (DictionaryEntry item in headers)
                    {
                        httpRequestMessage.Headers.Add(item.Key.ToString(), item.Value.ToString());
                    }
                }

                var cts = new CancellationTokenSource();
                // time out
                cts.CancelAfter(TimeSpan.FromSeconds(timeout.Value));

                result = await Client.SendAsync(httpRequestMessage, cts.Token);
                // thoát vòng lặp
                continueCall = false;
            }
            catch (Exception ex)
            {
                _logProvider.Custom("error", $"CallAPI url {url} queries {_stringProvider.SerializeUtf8(queries)} method {httpMethod}");
                // gọi lại nếu có lỗi
                if (count < 3 && recall.Value)
                {
                    count++;
                    queries = null;
                }
                else
                {
                    throw ex;
                }
            }
        }

        return result;
    }

    ///<inheritdoc/>
    public List<string> ConvertObjectToList(object data)
    {
        var result = new List<string>();

        if (data == null)
        {
            return result;
        }

        foreach (var item in data.GetType().GetProperties())
        {
            var val = item.GetValue(data);
            var name = item.Name;

            switch (val)
            {
                case null:
                    continue;
                case DateTime time:
                    var dateTime = time;
                    result.Add(name + "=" + dateTime.ToString("yyyy-MM-ddTHH:mm:ss.ffff"));
                    break;
                default:
                    val = HttpUtility.UrlEncode(val.ToString());
                    result.Add(name + "=" + val);
                    break;
            }
        }

        return result;
    }

    ///<inheritdoc/>
    public IList<KeyValuePair<string, string>> ConvertObjectToKeyValuePair(dynamic obj)
    {
        var result = new List<KeyValuePair<string, string>>();

        foreach (KeyValuePair<string, object> kvp in obj)
        {
            result.Add(new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString()));
        }

        return result;
    }

    ///<inheritdoc/>
    public async Task<T> ConvertHttpResponseMessageToObject<T>(HttpResponseMessage response)
    {
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(responseBody);
        return result;
    }
}
