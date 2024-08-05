using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CommonOperations.Messaging.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, Assembly? assembly = null)
        {
            //Implemted RabbitMQ MassTransit configuration
            return services;
        }
    }
}
