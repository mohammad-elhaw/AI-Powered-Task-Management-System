using DotNetCore.CAP;

namespace Shared.Messaging.Abstractions;

public interface ICapIntegrationEventSubscriber<in TIntegrationEvent> : ICapSubscribe
    where TIntegrationEvent :IIntegrationEvent
{
    [CapSubscribe(nameof(TIntegrationEvent))]
    public Task Handle(TIntegrationEvent @event);
}
