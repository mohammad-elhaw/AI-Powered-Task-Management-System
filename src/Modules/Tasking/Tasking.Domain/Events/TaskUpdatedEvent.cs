using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskUpdatedEvent(Aggregates.Task task) : IDomainEvent;