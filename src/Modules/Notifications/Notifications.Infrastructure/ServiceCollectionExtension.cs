using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Abstractions;
using Notifications.Application.Email;
using Notifications.Application.IntegrationEventHandlers;
using Notifications.Infrastructure.Email;
using Notifications.Infrastructure.Identity;
using Shared.Messaging;

namespace Notifications.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddNotificationsInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddScoped<TaskAssignedIntegrationEventHandler>();
        services.AddCapConsumer(config);

        services.AddScoped<IEmailSender, MailKitPapercutSender>();
        services.AddHttpClient<IUserDirectory, IdentityUserDirectory>(
            client =>
            {
                client.BaseAddress =
                    new Uri(config["Identity:BaseUrl"]!);
            });

        return services;
    }
}
