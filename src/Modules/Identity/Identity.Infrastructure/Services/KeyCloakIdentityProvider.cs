using Identity.Application.Abstractions;
using Identity.Application.Dtos;
using Identity.Infrastructure.Services.Roles;
using Identity.Infrastructure.Services.Users;

namespace Identity.Infrastructure.Services;

public sealed class KeyCloakIdentityProvider(
    KeycloakUserClient users,
    KeycloakRoleClient roles)
    : IIdentityProvider
{

    public async Task<string> CreateUser(string userName, string email, string firstName, 
        string lastName, string tempPassword, CancellationToken cancellationToken)
    {

        if (await users.UserExists(userName, cancellationToken))
            throw new InvalidOperationException($"User '{userName}' already exists.");

        var userPayload = new
        {
            username = userName,
            email = email,
            firstName = firstName,
            lastName = lastName,
            enabled = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = tempPassword,
                    temporary = true
                }
            }
        };

        return await users.CreateUser(userPayload, cancellationToken);
    }

    
    public async Task DeleteUser(string keyCloakUserId, CancellationToken cancellationToken)
        => await users.DeleteUser(keyCloakUserId, cancellationToken);


    public async Task<IReadOnlyList<UserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        var keyCloakUsers = await users.GetAllUsers(cancellationToken);

        return keyCloakUsers.Select(u => new UserDto
        (
            Id: Guid.Empty,
            KeycloakId: u.Id,
            Email: u.Email,
            FirstName: u.FirstName,
            LastName: u.LastName,
            IsActive: u.Enabled,
            RoleNames : new List<string>()
        )).ToList();
    }

    public async Task EnsureRealmRoleExists(string roleName, CancellationToken cancellationToken)
        => await roles.EnsureRealmRoleExists(roleName, cancellationToken);

    public async Task AssignRole(string keyCloakUserId, string roleName, CancellationToken cancellationToken)
        => await roles.AssignRole(keyCloakUserId, roleName, cancellationToken);

    public async Task<List<string>> GetUserRoles(string keycloakUserId, CancellationToken cancellationToken)
        => await roles.GetUserRoles(keycloakUserId, cancellationToken);
}
