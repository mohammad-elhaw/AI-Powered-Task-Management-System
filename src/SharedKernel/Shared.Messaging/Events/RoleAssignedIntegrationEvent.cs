using Shared.Messaging.Abstractions;

namespace Shared.Messaging.Events;

public record RoleAssignedIntegrationEvent(
    Guid UserId,
    string RoleName,
    DateTime OccurredOn): IIntegrationEvent;