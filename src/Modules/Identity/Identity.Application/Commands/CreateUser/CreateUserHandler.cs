using Identity.Application.Dtos;
using Identity.Domain.Aggregates;
using Identity.Domain.Repositories;
using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Commands.CreateUser;

public class CreateUserHandler
    (IRoleRepository roleRepository,
    IUserRepository userRepository,
    IIdentityProvider identityProvider)
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        string tempPassword = "12345";
        var keyCloakId = await identityProvider.CreateUser(
            command.UserName,
            command.Email,
            command.FirstName,
            command.LastName,
            tempPassword,
            cancellationToken);

        var user = User.Create(Guid.NewGuid(), keyCloakId, 
            Domain.ValueObjects.Email.Create(command.Email),
            Domain.ValueObjects.FullName.Create(command.FirstName, command.LastName));

        if(command.RoleNames != null && command.RoleNames.Count != 0)
        {
            foreach(var roleName in command.RoleNames.Distinct(StringComparer.OrdinalIgnoreCase))
            {
                var role = await roleRepository.GetByName(roleName, cancellationToken);
                if(role == null)
                {
                    role = Role.Create(Guid.NewGuid(), roleName);
                    await roleRepository.Add(role, cancellationToken);
                }

                await identityProvider.EnsureRealmRoleExists(role.Name, cancellationToken);

                await identityProvider.AssignRole(keyCloakId, role.Name, cancellationToken);
                user.AssignRole(role);
            }
        }

        await userRepository.Add(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);
        await roleRepository.SaveChanges(cancellationToken);

        return new CreateUserResult
        (
            UserDto: new UserDto
            (
                Id: user.Id,
                KeycloakId: user.KeycloakId,
                Email: user.Email.Value,
                FirstName: user.Name.FirstName,
                LastName: user.Name.LastName,
                IsActive: user.IsActive,
                RoleIds: user.UserRoles.Select(ur => ur.RoleId).ToList()
            )
        );
    }
}
