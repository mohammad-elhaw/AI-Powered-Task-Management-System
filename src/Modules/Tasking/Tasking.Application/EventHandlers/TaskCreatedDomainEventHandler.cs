using MediatR;
using Shared.Application.Abstractions.Events;
using Shared.Messaging.Abstractions;
using Shared.Messaging.Events;
using Tasking.Domain.Events;

namespace Tasking.Application.EventHandlers;

public sealed class TaskCreatedDomainEventHandler(
    IMessageBus messageBus)
    : INotificationHandler<DomainEventNotification<TaskCreatedDomainEvent>>
{
    public async Task Handle(DomainEventNotification<TaskCreatedDomainEvent> notification, 
        CancellationToken cancellationToken)
    {
        await messageBus.Publish(
            "user.created",
            new TaskCreatedIntegrationEvent(
                notification.DomainEvent.TaskId,
                notification.DomainEvent.AssignedUserId,
                notification.DomainEvent.Title),
            cancellationToken);
    }
}
