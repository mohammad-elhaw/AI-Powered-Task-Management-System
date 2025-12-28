using DotNetCore.CAP;
using Shared.Messaging.Abstractions;

namespace Shared.Messaging.CAP;

public class CapMessageBus(ICapPublisher capPublisher) : IMessageBus
{
    public async Task Publish<TIntegrationEvent>(string eventName, TIntegrationEvent @event, 
        CancellationToken cancellationToken = default) 
        where TIntegrationEvent : IIntegrationEvent
    {
        await capPublisher.PublishAsync(eventName, @event, cancellationToken: cancellationToken);
    }
}
