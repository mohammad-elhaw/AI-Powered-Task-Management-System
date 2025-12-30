using Shared.Domain.Events;

namespace Identity.Domain.Events;

public record RoleAssignedDomainEvent(
    Guid UserId,
    string RoleName
): IDomainEvent;
