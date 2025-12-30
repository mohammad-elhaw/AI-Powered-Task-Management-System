using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EventDispatcher;
using System.Reflection;

namespace Shared.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSharedInfrastructure(
        this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(cfg =>
        {
            var requiredAssemblies = assemblies
                .Where(assembly => !assembly.IsDynamic)
                .ToArray();

            cfg.RegisterServicesFromAssemblies(requiredAssemblies);
        });

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
