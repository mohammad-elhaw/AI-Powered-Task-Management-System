namespace Shared.Messaging.Abstractions;

public interface IIntegrationEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
    public string EventType => typeof(IIntegrationEvent).Name;
}
