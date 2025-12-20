namespace Identity.Application.Abstractions;

public interface IUserReadRepository
{
    Task<IReadOnlyDictionary<string, Guid>>
        GetUserIdsByKeycloakIds(
            IEnumerable<string> keycloakIds,
            CancellationToken cancellationToken);

    Task<IReadOnlyDictionary<string, List<string>>>
        GetRolesByKeycloakIds(List<string> keycloakIds,
        CancellationToken cancellationToken);
}
