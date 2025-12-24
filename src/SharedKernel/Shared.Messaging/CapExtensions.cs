using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Abstractions;
using Shared.Messaging.CAP;

namespace Shared.Messaging;

public static class CapExtensions
{
    public static IServiceCollection AddCapMessaging(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddCap(opts =>
        {
            opts.UsePostgreSql(cfg =>
            {
                cfg.ConnectionString = config.GetConnectionString("Database")!;
                cfg.Schema = "cap";
            });

            opts.UseRabbitMQ(cfg =>
            {
                cfg.HostName = config["MessageBroker:Host"]!;
                cfg.UserName = config["MessageBroker:Username"]!;
                cfg.Password = config["MessageBroker:Password"]!;
            });
        });
        services.AddScoped<IMessageBus, CapMessageBus>();
        return services;
    }
}
