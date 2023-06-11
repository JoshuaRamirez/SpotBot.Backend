using Microsoft.Extensions.DependencyInjection;
using SpotBot.Server.Configuration;

public static class SpotBotConfiguration
{
    private static IServiceProvider? _serviceProvider;

    public static void Initialize(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static SpotBotConfigurationModel Model
    {
        get
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider has not been initialized.");
            }
            var config = _serviceProvider.GetRequiredService<SpotBotConfigurationModel>();
            return config;
        }
    }
}
