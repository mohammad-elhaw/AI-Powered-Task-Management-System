using Shared.Domain.Events;

namespace Shared.Infrastructure.EventDispatcher;

public interface IDomainEventDispatcher
{
    Task Dispatch(IEnumerable<IDomainEvent> domainEvents);
}
