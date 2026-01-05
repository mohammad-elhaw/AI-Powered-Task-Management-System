using MediatR;
using Shared.Application.Abstractions.Events;
using Shared.Messaging.Abstractions;
using Shared.Messaging.Events;
using Tasking.Domain.Events;
using Tasking.Domain.Repositories;

namespace Tasking.Application.EventHandlers;

public sealed class TaskAssignedDomainEventHandler(
    IMessageBus messageBus,
    ITaskRepository taskRepository)
    : INotificationHandler<DomainEventNotification<TaskAssignedDomainEvent>>
{
    public async Task Handle(DomainEventNotification<TaskAssignedDomainEvent> notification, 
        CancellationToken cancellationToken)
    {

        var task = await taskRepository.GetTask(notification.DomainEvent.TaskId, cancellationToken);
        if (task is null)
        {
            // Handle the case where the task is not found, possibly log or throw an exception
            return;
        }

        var integrationEvent = new TaskAssignedIntegrationEvent
        (
            AssignedUserId: notification.DomainEvent.UserId,
            TaskTitle: task.Title.Value,
            DueDate: task.DueDate?.Value ?? DateTime.MinValue
        );

        await messageBus.Publish(
            "tasking.task.assigned", 
            integrationEvent, 
            cancellationToken);
    }
}
