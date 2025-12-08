namespace Shared.Domain.Events;

public interface IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public string EventName { get; }
}
