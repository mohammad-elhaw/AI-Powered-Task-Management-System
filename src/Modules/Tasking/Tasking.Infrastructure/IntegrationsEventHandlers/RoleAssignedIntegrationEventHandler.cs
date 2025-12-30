using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging.Events;
using Tasking.Infrastructure.Database;

namespace Tasking.Infrastructure.IntegrationsEventHandlers;

public class RoleAssignedIntegrationEventHandler(
    TaskingDbContext context)
    : ICapSubscribe
{

    [CapSubscribe("identity.role.assigned")]
    public async Task Handle(RoleAssignedIntegrationEvent @event)
    {
        // Here you can handle the event, e.g., update tasks based on the new role assignment
        // For example, you might want to log the event or update some task assignments
        // Example: Log the role assignment
        
        var exists = await context.UserRoles
            .AnyAsync(ur => ur.UserId == @event.UserId && ur.RoleName == @event.RoleName);

        if (exists) return;
        context.UserRoles.Add(new Domain.ReadModels.TaskingUserRole
        {
            UserId = @event.UserId,
            RoleName = @event.RoleName
        });

        await context.SaveChangesAsync();
    }
}
