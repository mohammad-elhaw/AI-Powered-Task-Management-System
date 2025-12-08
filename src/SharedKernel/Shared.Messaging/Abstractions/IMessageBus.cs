namespace Shared.Messaging.Abstractions;

public interface IMessageBus
{
    Task Publish<TIntegrationEvent>(TIntegrationEvent @event, 
        CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent;
}
