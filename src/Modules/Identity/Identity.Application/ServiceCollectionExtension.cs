using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
    {
        return services;
    }
}
