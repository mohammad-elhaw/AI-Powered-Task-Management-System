using Identity.Application.Abstractions;
using Identity.Application.Dtos;
using Identity.Domain.Aggregates;
using Identity.Domain.Repositories;
using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;

namespace Identity.Application.Commands.CreateUser;

public class CreateUserHandler
    (IRoleRepository roleRepository,
    IUserRepository userRepository,
    IIdentityProvider identityProvider)
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<Result<CreateUserResult>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        string? createdKeycloadId = null;
        User? createdUser = null;

        try
        {
            string tempPassword = "12345";
            var keycloakIdResult = await identityProvider.CreateUser(
                command.UserName,
                command.Email,
                command.FirstName,
                command.LastName,
                tempPassword,
                cancellationToken);

            if(keycloakIdResult.IsFailure)
                return Result<CreateUserResult>.Failure(keycloakIdResult.Error);

            createdKeycloadId = keycloakIdResult.Value!;

            var user = User.Create(Guid.NewGuid(), keycloakIdResult.Value!, 
                Domain.ValueObjects.Email.Create(command.Email),
                Domain.ValueObjects.FullName.Create(command.FirstName, command.LastName));

            createdUser = user;

            if (command.RoleNames != null && command.RoleNames.Count != 0)
            {
                foreach (var roleName in command.RoleNames.Distinct(StringComparer.OrdinalIgnoreCase))
                {
                    var role = await roleRepository.GetByName(roleName, cancellationToken);
                    if (role == null)
                    {
                        role = Role.Create(Guid.NewGuid(), roleName);
                        await roleRepository.Add(role, cancellationToken);
                    }

                    await identityProvider.EnsureRealmRoleExists(role.Name, cancellationToken);

                    await identityProvider.AssignRole(keycloakIdResult.Value!, role.Name, cancellationToken);
                    user.AssignRole(role);
                }
            }

            await userRepository.Add(user, cancellationToken);
            await userRepository.SaveChanges(cancellationToken);
            await roleRepository.SaveChanges(cancellationToken);

            return Result<CreateUserResult>.Success(new CreateUserResult
            (
                UserDto: new UserDto
                (
                    Id: user.Id,
                    KeycloakId: user.KeycloakId,
                    Email: user.Email.Value,
                    FirstName: user.Name.FirstName,
                    LastName: user.Name.LastName,
                    IsActive: user.IsActive,
                    RoleNames: user.UserRoles.Select(ur => ur.Role.Name).ToList()
                )
            ));
        }
        catch(Exception ex)
        {
            if (!string.IsNullOrEmpty(createdKeycloadId))
                await identityProvider.DeleteUser(createdKeycloadId, cancellationToken);
            
            if (createdUser != null)
                await userRepository.Delete(createdUser, cancellationToken);

            return Result<CreateUserResult>.Failure(new Error("CreatedUserFailed", ex.Message, default));
        }
    }
}
