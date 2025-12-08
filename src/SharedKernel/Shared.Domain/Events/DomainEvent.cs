namespace Shared.Domain.Events;

public class DomainEvent : IDomainEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime OccurredOn { get; protected set; }
    public string EventName => GetType().AssemblyQualifiedName!;
}
