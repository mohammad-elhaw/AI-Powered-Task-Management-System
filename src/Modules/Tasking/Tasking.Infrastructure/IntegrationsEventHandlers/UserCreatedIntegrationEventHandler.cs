using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging.Events;
using Tasking.Infrastructure.Database;
using Tasking.Infrastructure.Projections;

namespace Tasking.Infrastructure.IntegrationsEventHandlers;

public class UserCreatedIntegrationEventHandler(
    TaskingDbContext context)
    : ICapSubscribe
{

    [CapSubscribe("identity.user.created")]
    public async Task Handle(UserCreatedIntegrationEvent @event)
    {
        var exists = await context.TaskingUsers
            .AnyAsync(u => u.UserId == @event.UserId);

        if (exists) return;

        context.TaskingUsers.Add(new TaskingUserProjection(
            @event.UserId, 
            @event.DisplayName, 
            @event.Email, 
            @event.IsActive));

        await context.SaveChangesAsync();
    }
}
