using Microsoft.Extensions.DependencyInjection;
using Tasking.Application.Authorization;

namespace Tasking.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTaskingApplication(this IServiceCollection services)
    {
        services.AddScoped<ITaskAuthorizationPolicy, TaskAuthorizationPolicy>();
        return services;
    }
}
