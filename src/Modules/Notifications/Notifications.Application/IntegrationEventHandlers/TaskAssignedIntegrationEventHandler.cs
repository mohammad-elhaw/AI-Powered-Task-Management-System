using DotNetCore.CAP;
using Notifications.Application.Abstractions;
using Notifications.Application.Email;
using Shared.Messaging.Events;

namespace Notifications.Application.IntegrationEventHandlers;

public class TaskAssignedIntegrationEventHandler(
    IUserDirectory userDirectory,
    IEmailSender emailSender)
    : ICapSubscribe
{
    [CapSubscribe("tasking.task.assigned")]
    public async Task Handle(TaskAssignedIntegrationEvent @event, CancellationToken cancellationToken)
    {

        var user = await userDirectory.GetUserContact(@event.AssignedUserId);
        if (user == null || string.IsNullOrWhiteSpace(user.Email)) return;

        var subject = "New Task Assigned";
        var body = $"""
            <h2>New Task Assigned</h2>
            <p><b>Task:</b> {@event.TaskTitle}</p>
            <p><b>Due Date:</b> {@event.DueDate:d}</p>
        """;


        await emailSender.SendEmail(
            user.Email,
            subject,
            body,
            cancellationToken);
    }
}
