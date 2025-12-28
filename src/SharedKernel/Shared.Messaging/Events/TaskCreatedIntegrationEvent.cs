using Shared.Messaging.Abstractions;

namespace Shared.Messaging.Events;

public sealed record TaskCreatedIntegrationEvent(
    Guid TaskId,
    Guid AssignedUserId,
    string Title) : IIntegrationEvent;