using Identity.Domain.Events;
using MediatR;
using Shared.Application.Abstractions.Events;
using Shared.Messaging.Abstractions;
using Shared.Messaging.Events;

namespace Identity.Application.DomainEventHandlers;

public class UserActivatedDomainEventHandler
    (IMessageBus messageBus)
    : INotificationHandler<DomainEventNotification<UserActivatedDomainEvent>>
{
    public Task Handle(DomainEventNotification<UserActivatedDomainEvent> notification, 
        CancellationToken cancellationToken)
    {
        return messageBus.Publish(
            "identity.user.activated",
            new UserActivatedIntegrationEvent
            (
                notification.DomainEvent.UserId
            ), cancellationToken);
    }
}