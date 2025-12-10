using Microsoft.Extensions.DependencyInjection;

namespace Tasking.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTaskingApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));
        return services;
    }
}
