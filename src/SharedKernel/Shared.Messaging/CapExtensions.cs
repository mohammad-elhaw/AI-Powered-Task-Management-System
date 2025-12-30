using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Abstractions;
using Shared.Messaging.CAP;

namespace Shared.Messaging;

public static class CapExtensions
{
    public static IServiceCollection AddCapMessaging<TDbContext>(this IServiceCollection services,
        IConfiguration config)
        where TDbContext : DbContext
    {
        services.AddCap(opts =>
        {
            opts.UseEntityFramework<TDbContext>();

            opts.UseRabbitMQ(cfg =>
            {
                cfg.HostName = config["MessageBroker:Host"]!;
                cfg.Port = int.Parse(config["MessageBroker:Port"]?? "5672");
                cfg.UserName = config["MessageBroker:Username"]!;
                cfg.Password = config["MessageBroker:Password"]!;
                cfg.VirtualHost = config["MessageBroker:VirtualHost"]?? "/";
            });
        });
        services.AddScoped<IMessageBus, CapMessageBus>();
        return services;
    }
}
