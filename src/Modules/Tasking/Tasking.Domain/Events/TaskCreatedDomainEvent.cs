using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskCreatedDomainEvent(Guid TaskId, string Title)
    : IDomainEvent;