namespace Notifications.Application.Abstractions;

public interface IUserDirectory
{
    Task<UserContact?> GetUserContact(Guid userId);
}

public sealed record UserContact(Guid UserId, string Email);