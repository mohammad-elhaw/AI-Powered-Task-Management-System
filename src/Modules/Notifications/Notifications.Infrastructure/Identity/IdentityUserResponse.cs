namespace Notifications.Infrastructure.Identity;

internal sealed record IdentityUserResponse(
    Guid Id,
    string Email);