using Identity.Application.Abstractions.IdentityProvider;
using Identity.Application.Abstractions.IdentityProvider.CreateUser;
using Identity.Application.Abstractions.IdentityProvider.GetAllUsers;
using Identity.Application.Errors;
using Identity.Infrastructure.Services.Roles;
using Identity.Infrastructure.Services.Users;
using Shared.Application.Results;

namespace Identity.Infrastructure.Services;

internal sealed class KeyCloakIdentityProvider(
    KeycloakUserClient users,
    KeycloakRoleClient roles)
    : IIdentityProvider
{

    public async Task<Result<string>> CreateUser(Request request, CancellationToken cancellationToken)
    {

        if (await users.UserExists(new UserExistsRequest(request.UserName), cancellationToken))
            return Result<string>.Failure(UserErrors.UserAlreadyExists);

        var userPayload = new
        {
            username = request.UserName,
            email = request.Email,
            firstName = request.FirstName,
            lastName = request.LastName,
            enabled = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = request.TempPassword,
                    temporary = true
                }
            }
        };
        
        var keycloakId = await users.CreateUser(userPayload, cancellationToken);
        return Result<string>.Success(keycloakId);
        
    }

    public async Task Deactivate(string keyCloakUserId, CancellationToken cancellationToken)
        => await users.DeactivateUser(keyCloakUserId, cancellationToken);

    public async Task DeleteUser(string keyCloakUserId, CancellationToken cancellationToken)
        => await users.DeleteUser(keyCloakUserId, cancellationToken);


    public async Task<Response> GetAllUsers(CancellationToken cancellationToken)
    {
        var keyCloakUsers = await users.GetAllUsers(cancellationToken);

        var response = keyCloakUsers.Select(u => new UserDto
        (
            Id: Guid.Empty,
            KeycloakId: u.Id,
            Email: u.Email,
            FirstName: u.FirstName,
            LastName: u.LastName,
            IsActive: u.Enabled,
            RoleNames : new List<string>()
        )).ToList();

        return new Response(response);
    }

    public async Task EnsureRealmRoleExists(string roleName, CancellationToken cancellationToken)
        => await roles.EnsureRealmRoleExists(roleName, cancellationToken);

    public async Task AssignRole(string keyCloakUserId, string roleName, CancellationToken cancellationToken)
        => await roles.AssignRole(keyCloakUserId, roleName, cancellationToken);

    public async Task<List<string>> GetUserRoles(string keycloakUserId, CancellationToken cancellationToken)
        => await roles.GetUserRoles(keycloakUserId, cancellationToken);
}
