using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskCompletedEvent(Aggregates.Task task) : IDomainEvent;