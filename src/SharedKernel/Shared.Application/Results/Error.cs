namespace Shared.Application.Results;

public record Error(string Code, string Message, object? details)
{
    public static readonly Error None = new (string.Empty, string.Empty, default);
}
