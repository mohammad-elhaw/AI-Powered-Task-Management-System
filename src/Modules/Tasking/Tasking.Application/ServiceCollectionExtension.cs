using Microsoft.Extensions.DependencyInjection;

namespace Tasking.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTaskingApplication(this IServiceCollection services)
    {
        return services;
    }
}
