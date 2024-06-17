using Draft.Core.Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class ConfigProvider : IConfigProvider
{
    /// <summary>
    /// ILogger
    /// </summary>
    private ILogger<ConfigProvider> Logger { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    public ConfigProvider(ILogger<ConfigProvider> logger)
    {
        Logger = logger;
    }

    ///<inheritdoc/>
    public object? GetConfig(string key)
    {
        try
        {
            var path = Directory.GetCurrentDirectory();
            string text = File.ReadAllText($@"{path}\\config.json");
            dynamic d = JObject.Parse(text);
            return d[key];
        }
        catch (Exception ex)
        {
            Logger.LogError("error", ex.ToString());
            return null;
        }
    }

    ///<inheritdoc/>
    public string? GetConfigString(string key)
    {
        try
        {
            return GetConfig(key)?.ToString();
        }
        catch (Exception ex)
        {
            Logger.LogError("error", ex.ToString());
            return null;
        }
    }

    ///<inheritdoc/>
    public void SaveConfig(object data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(@"config.json", json);
    }
}
