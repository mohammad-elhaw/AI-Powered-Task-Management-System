using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));
        return services;
    }
}
