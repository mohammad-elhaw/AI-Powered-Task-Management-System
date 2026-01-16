using Shared.Domain.Abstractions;
using Tasking.Domain.Exceptions;

namespace Tasking.Domain.Entities;

public class TaskItem : Entity<int>
{
    public Guid TaskId { get; private set; }
    public string Content { get; private set; }
    public bool IsCompleted { get; private set; }

    private TaskItem() { }

    internal TaskItem(Guid taskId, string content)
    {
        if(string.IsNullOrWhiteSpace(content))
            throw new InvalidTaskItemContentException("Task item content cannot be null or empty.");

        TaskId = taskId;
        Content = content;
        IsCompleted = false;
    }

    public void MarkAsCompleted() =>
        IsCompleted = true;
}
