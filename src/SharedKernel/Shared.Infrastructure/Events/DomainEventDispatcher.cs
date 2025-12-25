using MediatR;
using Shared.Domain.Events;

namespace Shared.Infrastructure.Events;

public class DomainEventDispatcher(IMediator mediator)
    : IDomainEventDispatcher
{
    public async Task Dispatch<TDomainEvent>(IEnumerable<TDomainEvent> domainEvents) 
        where TDomainEvent : IDomainEvent
    {
        foreach(var domainEvent in domainEvents)
        {
            await mediator.Publish(new DomainEventNotification(domainEvent));
        }
    }
}
