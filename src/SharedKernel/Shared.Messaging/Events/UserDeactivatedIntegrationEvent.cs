using Shared.Messaging.Abstractions;

namespace Shared.Messaging.Events;

public record UserDeactivatedIntegrationEvent(
    Guid UserId)
    : IIntegrationEvent;