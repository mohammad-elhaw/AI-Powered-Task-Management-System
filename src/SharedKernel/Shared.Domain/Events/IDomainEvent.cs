namespace Shared.Domain.Events;

public interface IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
    public string EventName => GetType().AssemblyQualifiedName!;
}
