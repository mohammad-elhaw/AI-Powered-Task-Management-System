using Shared.Application.Results;

namespace Shared.Application.Security;

public static class SecurityErrors
{
    public static Error Unauthenticated =>
        new("Auth.Unauthenticated", "User is not authenticated", default);

    public static Error Forbidden(string permission) =>
        new("Auth.Forbidden", $"Missing permission: {permission}", default);
}