using MediatR;
using Shared.Domain.Events;

namespace Shared.Infrastructure.Events;

public sealed record DomainEventNotification(
    IDomainEvent DomainEvent) : INotification;
