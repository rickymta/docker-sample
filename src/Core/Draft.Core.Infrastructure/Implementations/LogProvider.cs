using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Draft.Core.Infrastructure.Abstractions;
using Serilog;
using System.Text;
using System.Text.Json;

namespace Draft.Core.Infrastructure.Implementations;

///<inheritdoc/>
internal class LogProvider : ILogProvider
{
    /// <summary>
    /// PatternLog
    /// </summary>
    private const string PatternLog = "{message} {data}";

    ///<inheritdoc/>
    public void Custom(string key, string message, object data, string? fileName = null)
    {
        var logger = SetLoggerConfig(key, fileName);
        logger.Information(PatternLog, message, data);
        logger.Dispose();
    }

    ///<inheritdoc/>
    public void Custom(string key, object data, bool json = false, string? fileName = null)
    {
        var logger = SetLoggerConfig(key, fileName);

        if (json)
        {
            data = JsonSerializer.Serialize(data);
        }

        logger.Information("{message}", data);
        logger.Dispose();
    }

    ///<inheritdoc/>
    public void Error(string key, object data, bool json = false)
    {
        var logger = SetLoggerConfig($"error/{key}");

        if (json)
        {
            data = JsonSerializer.Serialize(data);
        }

        logger.Error("{message}", data);
        logger.Dispose();
    }

    ///<inheritdoc/>
    public void Error(Exception ex)
    {
        var logger = SetLoggerConfig("Error", "Exception");
        logger.Error(ex.Message);
        logger.Error(ex.StackTrace);
        logger.Dispose();
    }

    ///<inheritdoc/>
    public void ErrorWithKey(string key, Exception ex)
    {
        var logger = SetLoggerConfig($"error/{key}");
        logger.Error(ex.Message);
        logger.Error(ex.StackTrace);
        logger.Dispose();
    }

    /// <summary>
    /// SetLoggerConfig
    /// </summary>
    /// <param name="key"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static Serilog.Core.Logger SetLoggerConfig(string key, string? fileName = null)
    {
        var expr = "key = '" + key + "'";
        string nameAdd = fileName ?? "log_";
        string fileNameLog = $"log/{key}/{nameAdd}.txt";
        string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Message:lj} {NewLine}";
        var logger = new LoggerConfiguration()
            .Enrich.WithProperty("key", key)
            .Filter.ByIncludingOnly(expr)
            .WriteTo.Async(a => a.File(fileNameLog, rollingInterval: RollingInterval.Day, shared: true, outputTemplate: outputTemplate,
                                encoding: Encoding.UTF8, rollOnFileSizeLimit: true, fileSizeLimitBytes: 200 * 1024 * 1024))
            .CreateLogger();

        return logger;
    }
}
