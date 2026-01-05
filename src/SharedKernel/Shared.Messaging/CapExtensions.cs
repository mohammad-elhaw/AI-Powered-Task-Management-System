using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Abstractions;
using Shared.Messaging.CAP;

namespace Shared.Messaging;

public static class CapExtensions
{
    public static IServiceCollection AddCapPublisher<TDbContext>(this IServiceCollection services,
        IConfiguration config)
        where TDbContext : DbContext
    {
        services.AddCap(opts =>
        {
            opts.UseEntityFramework<TDbContext>();
            ConfigureRabbitMq(opts, config);
        });
        services.AddScoped<IMessageBus, CapMessageBus>();
        return services;
    }

    public static IServiceCollection AddCapConsumer(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddCap(options =>
        {
            ConfigureRabbitMq(options, config);
        });

        return services;
    }

    private static void ConfigureRabbitMq(CapOptions options, IConfiguration config)
    {
        options.UseRabbitMQ(cfg =>
        {
            cfg.HostName = config["MessageBroker:Host"]!;
            cfg.Port = int.Parse(config["MessageBroker:Port"] ?? "5672");
            cfg.UserName = config["MessageBroker:Username"]!;
            cfg.Password = config["MessageBroker:Password"]!;
            cfg.VirtualHost = config["MessageBroker:VirtualHost"] ?? "/";
        });
    }
}
