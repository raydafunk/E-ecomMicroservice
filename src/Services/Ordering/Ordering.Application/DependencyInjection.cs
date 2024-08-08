using CommonOperations.Behavior;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CommonOperations.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
