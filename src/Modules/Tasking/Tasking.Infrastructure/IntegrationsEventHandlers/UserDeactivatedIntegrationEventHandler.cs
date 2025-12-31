using DotNetCore.CAP;
using Shared.Messaging.Events;
using Tasking.Infrastructure.Database;

namespace Tasking.Infrastructure.IntegrationsEventHandlers;

internal class UserDeactivatedIntegrationEventHandler
    (TaskingDbContext context)
    : ICapSubscribe
{
    [CapSubscribe("identity.user.deactivated")]
    public async Task Handle(UserDeactivatedIntegrationEvent @event)
    {
        var user = await context.TaskingUsers
            .FindAsync(@event.UserId);
        if (user is null) return;
        user.Deactivate();
        await context.SaveChangesAsync();
    }
}
