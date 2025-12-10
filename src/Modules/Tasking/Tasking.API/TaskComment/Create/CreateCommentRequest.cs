namespace Tasking.API.TaskComment.Create;

public record CreateCommentRequest
{
    public string Content { get; init; } = default!;
    public string? Author { get; init; }
}
