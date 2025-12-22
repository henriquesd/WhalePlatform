using WP.Core.Utils;
using WP.MessageBus;

namespace WP.Customer.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMessageBus>(new MessageBus.MessageBus(
                configuration.GetMessageQueueConnection("MessageBus")));
        }
    }
}
