using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskCreatedEvent(Guid Id) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;

    public string EventName => nameof(TaskCreatedEvent);
}