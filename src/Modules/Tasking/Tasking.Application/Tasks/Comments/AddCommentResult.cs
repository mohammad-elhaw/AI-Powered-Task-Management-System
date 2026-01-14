namespace Tasking.Application.Tasks.Comments;

public record AddCommentResult(CommentDto CommentDto);

public record CommentDto(int Id, string Content, string Author);