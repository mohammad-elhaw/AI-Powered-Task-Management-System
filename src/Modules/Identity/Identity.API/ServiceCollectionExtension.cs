using Identity.Application;
using Identity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIdentityAPI(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddIdentityApplication();
        services.AddIdentityInfrastructure(config);
        return services;
    }

}
