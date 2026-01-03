using DotNetCore.CAP;
using Shared.Messaging.Events;
using Tasking.Infrastructure.Database;

namespace Tasking.Infrastructure.IntegrationsEventHandlers;

internal class UserActivatedIntegrationEventHandler
    (TaskingDbContext context)
    : ICapSubscribe
{
    [CapSubscribe("identity.user.activated")]
    public async Task Handle(UserActivatedIntegrationEvent @event)
    {
        var user = await context.TaskingUsers
            .FindAsync(@event.UserId);
        if (user is null) return;
        user.Activate();
        await context.SaveChangesAsync();
    }
}
