using Shared.Domain.Events;

namespace Identity.Domain.Events;

public record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;