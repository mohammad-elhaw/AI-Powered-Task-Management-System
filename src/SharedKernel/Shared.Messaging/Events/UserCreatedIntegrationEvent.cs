using Shared.Messaging.Abstractions;

namespace Shared.Messaging.Events;

public record UserCreatedIntegrationEvent(
    Guid UserId,
    string DisplayName,
    string Email,
    bool IsActive = true)
    : IIntegrationEvent;