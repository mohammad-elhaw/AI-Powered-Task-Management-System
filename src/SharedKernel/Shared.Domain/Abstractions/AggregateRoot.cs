using Shared.Domain.Events;

namespace Shared.Domain.Abstractions;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    protected AggregateRoot() : base() { }
    protected AggregateRoot(TId id) : base(id) { }

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        AddDomainEvent(domainEvent);
}
