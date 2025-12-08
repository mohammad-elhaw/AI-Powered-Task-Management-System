using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskCompletedEvent(Guid Id) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;

    public string EventName => nameof(TaskCompletedEvent);
}
