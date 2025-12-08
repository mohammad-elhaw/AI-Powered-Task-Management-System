using Shared.Domain.Events;

namespace Shared.Domain.Abstractions;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }

    protected Entity() { }
    protected Entity(TId id)
    {
        Id = id;
    }

    private readonly List<IDomainEvent> domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) =>
        domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => domainEvents.Clear();
}
