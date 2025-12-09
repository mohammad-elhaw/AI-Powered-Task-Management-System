using Shared.Domain.Abstractions;

namespace Tasking.Domain.Entities;

public class TaskItem : Entity<int>
{
    public Guid TaskId { get; private set; }
    public string Content { get; private set; }
    public bool IsCompleted { get; private set; }

    private TaskItem() { }

    internal TaskItem(Guid taskId, string content)
    {
        TaskId = taskId;
        Content = content;
        IsCompleted = false;
    }

    public void MarkAsCompleted() =>
        IsCompleted = true;
}
