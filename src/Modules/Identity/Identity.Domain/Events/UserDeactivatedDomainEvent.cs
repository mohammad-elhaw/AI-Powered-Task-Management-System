using Shared.Domain.Events;

namespace Identity.Domain.Events;

public record UserDeactivatedDomainEvent(Guid UserId) : IDomainEvent;