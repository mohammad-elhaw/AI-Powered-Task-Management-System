using Shared.Messaging.Abstractions;

namespace Shared.Messaging.Events;

public record UserActivatedIntegrationEvent(Guid UserId)
    : IIntegrationEvent;