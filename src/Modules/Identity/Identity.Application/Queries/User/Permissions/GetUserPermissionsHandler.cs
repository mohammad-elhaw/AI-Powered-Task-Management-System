using Identity.Application.Abstractions.Permissions;
using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;

namespace Identity.Application.Queries.User.Permissions;

public class GetUserPermissionsHandler
    (IUserPermissionsRepository userPermissionsRepository)
    : IQueryHandler<GetUserPermissionsQuery, IReadOnlySet<string>>
{
    public async Task<Result<IReadOnlySet<string>>> Handle(GetUserPermissionsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var userPermissions = await userPermissionsRepository.GetUserPermissions(query.KeycloakId, cancellationToken);
            return Result<IReadOnlySet<string>>.Success(userPermissions);
        }
        catch
        {
            return Result<IReadOnlySet<string>>.Failure(
                new Error(
                    "Error.InternalServerError",
                    "GetUserPermissionsError", default));
        }

    }
}
