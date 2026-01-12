using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Queries.User.Permissions;

public record GetUserPermissionsQuery(string KeycloakId) : IQuery<IReadOnlySet<string>>;