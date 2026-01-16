using Shared.Domain.Abstractions;
using Tasking.Domain.Exceptions;

namespace Tasking.Domain.Entities;

public class Comment : Entity<int>
{
    public Guid TaskId { get; private set; }
    public string Author { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private Comment() { }
    internal Comment(Guid taskId, string author, string content)
    {
        if(string.IsNullOrEmpty(author))
            throw new InvalidAuthorException("Author cannot be null or empty.");

        if(string.IsNullOrEmpty(content))
            throw new InvalidContentException("Content cannot be null or empty.");

        TaskId = taskId;
        Author = author;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}
