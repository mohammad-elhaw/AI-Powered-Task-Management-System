using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Abstractions;
using Shared.Messaging.CAP;
using Tasking.Infrastructure.Database;

namespace Tasking.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTaskingInfrastructure(this IServiceCollection services,
        IConfiguration config)
    {

        services.AddDbContext<TaskingDbContext>(opts =>
        {
            opts.UseNpgsql(config.GetConnectionString("Database"));
        });

        services.AddCap(opts =>
        {
            opts.UsePostgreSql(config.GetConnectionString("Database")!);
            opts.UseRabbitMQ(config.GetConnectionString("MessageBroker")!);
        });

        services.AddScoped<IMessageBus, CapMessageBus>();

        return services;
    }
}
