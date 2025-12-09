using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskCreatedEvent(Aggregates.Task task) : IDomainEvent;