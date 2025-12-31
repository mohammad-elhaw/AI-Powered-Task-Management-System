using Identity.Domain.Events;
using Identity.Domain.Repositories;
using MediatR;
using Shared.Application.Abstractions.Events;
using Shared.Messaging.Abstractions;
using Shared.Messaging.Events;

namespace Identity.Application.DomainEventHandlers;

public class UserCreatedDomainEventHandler(
    IMessageBus messageBus,
    IUserRepository userRepo)
    : INotificationHandler<DomainEventNotification<UserCreatedDomainEvent>>
{
    public async Task Handle(DomainEventNotification<UserCreatedDomainEvent> notification, 
        CancellationToken cancellationToken)
    {
        var user = await userRepo.GetById(notification.DomainEvent.UserId, cancellationToken);

        await messageBus.Publish(
            "identity.user.created",
            new UserCreatedIntegrationEvent
            (
                UserId: user!.Id,
                DisplayName: string.Concat(user.Name.FirstName, " ", user.Name.LastName),
                Email: user.Email.Value,
                IsActive: user.IsActive
            ), cancellationToken);
    }
}
