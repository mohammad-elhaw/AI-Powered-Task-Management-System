using Shared.Messaging.Abstractions;

namespace Shared.Messaging.Events;

public record TaskAssignedIntegrationEvent(
    Guid AssignedUserId,
    string TaskTitle,
    DateTime DueDate
) : IIntegrationEvent;