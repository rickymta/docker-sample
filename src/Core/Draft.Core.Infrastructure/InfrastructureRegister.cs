using Draft.Core.Infrastructure.Abstractions;
using Draft.Core.Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Draft.Core.Infrastructure;

/// <summary>
/// ProviderRegister
/// </summary>
public static class InfrastructureRegister
{
    /// <summary>
    /// AddProviderServices
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterProviders(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddHttpContextAccessor();
        services.AddTransient<IConfigProvider, ConfigProvider>();
        services.AddTransient<ICookieProvider, CookieProvider>();
        services.AddTransient<IHashProvider, HashProvider>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<ILogProvider, LogProvider>();
        services.AddTransient<IMemCacheProvider, MemCacheProvider>();
        services.AddTransient<IStringProvider, StringProvider>();
    }
}
