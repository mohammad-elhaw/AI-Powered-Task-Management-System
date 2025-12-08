using Shared.Domain.Abstractions;

namespace Tasking.Domain.Entities;

public class Comment : Entity<int>
{
    public Guid TaskId { get; private set; }
    public string Author { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private Comment() { }
    public Comment(Guid taskId, string author, string content)
    {
        TaskId = taskId;
        Author = author;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}
