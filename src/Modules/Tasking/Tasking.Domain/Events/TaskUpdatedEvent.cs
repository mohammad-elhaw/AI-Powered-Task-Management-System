using Shared.Domain.Events;

namespace Tasking.Domain.Events;

public record TaskUpdatedEvent(Guid Id) : IDomainEvent
{   
    public DateTime OccurredOn => DateTime.UtcNow;

    public string EventName => nameof(TaskUpdatedEvent);
}