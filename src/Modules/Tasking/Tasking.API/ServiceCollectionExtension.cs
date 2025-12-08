using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasking.Application;
using Tasking.Infrastructure;

namespace Tasking.API;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTaskingAPI(this IServiceCollection services,
        IConfiguration config)
    {

        services.AddTaskingApplication();
        services.AddTaskingInfrastructure(config);
        return services;
    }
}
