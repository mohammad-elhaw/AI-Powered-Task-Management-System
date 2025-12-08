using MediatR;
using Shared.Domain.Events;

namespace Shared.Application.Abstractions.Events;

public class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent)
    : INotification
    where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}
