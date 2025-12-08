using Shared.Domain.Events;

namespace Shared.Infrastructure.Events;

public interface IDomainEventDispatcher
{
    Task Dispatch<TDomainEvent>(IEnumerable<TDomainEvent> domainEvents)
        where TDomainEvent : IDomainEvent;
}
