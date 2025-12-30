using MediatR;
using Shared.Application.Abstractions.Events;
using Shared.Domain.Events;

namespace Shared.Infrastructure.EventDispatcher;

public class DomainEventDispatcher(IMediator mediator)
    : IDomainEventDispatcher
{
    public async Task Dispatch(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach(var domainEvent in domainEvents)
        {
            var notificationType = typeof(DomainEventNotification<>)
                .MakeGenericType(domainEvent.GetType());

            var notification = (INotification)Activator.CreateInstance(notificationType, domainEvent)!;

            await mediator.Publish(notification);
        }
    }
}
