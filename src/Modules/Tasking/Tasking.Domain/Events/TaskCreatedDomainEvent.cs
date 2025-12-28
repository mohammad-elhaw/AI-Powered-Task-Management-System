using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskCreatedDomainEvent(Guid TaskId, Guid AssignedUserId, string Title)
    : IDomainEvent;