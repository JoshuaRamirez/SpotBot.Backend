using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SpotBot.Server.Core
{
    public static class SpotBotLogging
    {
        private static IServiceProvider? _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentException("Service provider has not been initialized.");
            }
            _serviceProvider = serviceProvider;
        }

        public static ILogger<T> CreateLogger<T>()
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider has not been initialized.");
            }
            return _serviceProvider.GetRequiredService<ILogger<T>>();
        }
    }
}
