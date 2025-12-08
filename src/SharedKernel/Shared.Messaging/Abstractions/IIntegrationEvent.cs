namespace Shared.Messaging.Abstractions;

public interface IIntegrationEvent
{
    public Guid EventId { get; }
    public DateTime OccurredOn { get; }
    public string EventType { get; }
}
