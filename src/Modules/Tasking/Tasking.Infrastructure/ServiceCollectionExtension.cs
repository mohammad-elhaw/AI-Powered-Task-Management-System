using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Abstractions;
using Shared.Messaging.CAP;
using Tasking.Domain.Repositories;
using Tasking.Infrastructure.Database;
using Tasking.Infrastructure.Repositories;

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

        services.AddScoped<ITaskRepository, TaskRepository>();

        return services;
    }
}
