using DotNetCore.CAP;
using Shared.Messaging.Abstractions;

namespace Shared.Messaging.CAP;

public class CapMessageBus(ICapPublisher capPublisher) : IMessageBus
{
    public async Task Publish<TIntegrationEvent>(TIntegrationEvent @event, 
        CancellationToken cancellationToken = default) 
        where TIntegrationEvent : IIntegrationEvent
    {
        string eventName = @event.GetType().Name;
        await capPublisher.PublishAsync(eventName, @event, cancellationToken: cancellationToken);
    }
}
