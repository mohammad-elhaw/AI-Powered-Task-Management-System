using Identity.Domain.Events;
using MediatR;
using Shared.Application.Abstractions.Events;
using Shared.Messaging.Abstractions;
using Shared.Messaging.Events;

namespace Identity.Application.DomainEventHandlers;

internal class UserDeactivatedDomainEventHandler(
    IMessageBus messageBus)
    : INotificationHandler<DomainEventNotification<UserDeactivatedDomainEvent>>
{
    public Task Handle(DomainEventNotification<UserDeactivatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        return messageBus.Publish(
            "identity.user.deactivated",
            new UserDeactivatedIntegrationEvent
            (
                UserId: notification.DomainEvent.UserId
            ), cancellationToken);
    }
}
