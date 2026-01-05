using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskAssignedDomainEvent(Guid TaskId, Guid UserId): IDomainEvent;