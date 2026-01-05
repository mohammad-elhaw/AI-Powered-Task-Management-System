using MediatR;
using Shared.Application.Abstractions.Events;
using Tasking.Domain.Events;

namespace Tasking.Application.EventHandlers;

public sealed class TaskCreatedDomainEventHandler
    : INotificationHandler<DomainEventNotification<TaskCreatedDomainEvent>>
{
    public async Task Handle(DomainEventNotification<TaskCreatedDomainEvent> notification, 
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
