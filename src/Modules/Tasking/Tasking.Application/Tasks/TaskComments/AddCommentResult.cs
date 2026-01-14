namespace Tasking.Application.Tasks.TaskComments;

public record AddCommentResult(CommentDto CommentDto);

public record CommentDto(int Id, string Content, string Author);