using Identity.Application.Queries.User.Permissions;
using MediatR;
using Shared.Application.Results;
using Shared.Application.Security;

namespace Identity.Infrastructure.Services;

public class UserPermissionService(
    IMediator mediator)
    : IUserPermissionService
{
    public async Task<Result<IReadOnlySet<string>>> GetUserPermissions(string userKeycloakId, CancellationToken cancellationToken = default)
    {
        var userPermissionsResult = await mediator.Send(new GetUserPermissionsQuery(userKeycloakId), cancellationToken);
        
        if(userPermissionsResult.IsFailure)
            return Result<IReadOnlySet<string>>.Failure(userPermissionsResult.Error);
        
        return Result<IReadOnlySet<string>>.Success(userPermissionsResult.Value!);
    }
}
