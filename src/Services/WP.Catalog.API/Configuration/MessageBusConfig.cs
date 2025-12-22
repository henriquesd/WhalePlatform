using WP.Catalog.API.Services;
using WP.Core.Utils;
using WP.MessageBus;

namespace WP.Catalog.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CatalogIntegrationHandler>();
        }
    }
}