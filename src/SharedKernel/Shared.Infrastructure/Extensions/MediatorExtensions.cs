using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behaviors;
using System.Reflection;

namespace Shared.Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediatorAssemblies(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMediatR(cfg =>
        {
            var requiredAssemblies = assemblies
                .Where(assembly => !assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
                .ToArray();

            cfg.RegisterServicesFromAssemblies(requiredAssemblies);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}
