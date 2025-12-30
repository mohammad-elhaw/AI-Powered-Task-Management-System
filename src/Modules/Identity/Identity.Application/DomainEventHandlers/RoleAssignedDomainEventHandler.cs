using Identity.Domain.Events;
using MediatR;
using Shared.Application.Abstractions.Events;
using Shared.Messaging.Abstractions;
using Shared.Messaging.Events;

namespace Identity.Application.DomainEventHandlers;

public class RoleAssignedDomainEventHandler(
    IMessageBus messageBus)
    : INotificationHandler<DomainEventNotification<RoleAssignedDomainEvent>>
{
    public async Task Handle(DomainEventNotification<RoleAssignedDomainEvent> notification, CancellationToken cancellationToken)
    {
        await messageBus.Publish(
            "identity.role.assigned",
            new RoleAssignedIntegrationEvent(
                UserId: notification.DomainEvent.UserId,
                RoleName: notification.DomainEvent.RoleName,
                OccurredOn: DateTime.UtcNow)
            , cancellationToken);
    }
}