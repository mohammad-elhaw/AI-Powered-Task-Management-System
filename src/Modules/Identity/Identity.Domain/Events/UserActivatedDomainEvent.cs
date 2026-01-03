using Shared.Domain.Events;

namespace Identity.Domain.Events;

public record UserActivatedDomainEvent(Guid UserId) : IDomainEvent;