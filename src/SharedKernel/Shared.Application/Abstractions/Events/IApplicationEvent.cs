using MediatR;

namespace Shared.Application.Abstractions.Events;

public interface IApplicationEvent : INotification
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName!;
}
