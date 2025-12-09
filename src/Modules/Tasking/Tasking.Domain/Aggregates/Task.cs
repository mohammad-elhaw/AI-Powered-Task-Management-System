using Shared.Domain.Abstractions;
using Tasking.Domain.Entities;
using Tasking.Domain.Enums;
using Tasking.Domain.Events;
using Tasking.Domain.ValueObjects;

namespace Tasking.Domain.Aggregates;

public class Task : AggregateRoot<Guid>
{
    public TaskTitle Title { get; private set; }
    public TaskDescription Description { get; private set; }
    public Priority Priority { get; private set; }
    public Enums.TaskStatus Status { get; private set; }
    public DueDate? DueDate { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; } = default!;


    private readonly List<TaskItem> _items = [];
    public IReadOnlyList<TaskItem> Items => _items.AsReadOnly();

    private readonly List<Comment> _comments = [];
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();

    private Task() { }
    
    public static Task Create(TaskTitle title, TaskDescription description,
        Priority priority, DueDate dueDate)
    {
        Task task = new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            CreatedAt = DateTime.UtcNow,
            Priority = priority,
            DueDate = dueDate,
            Status = Enums.TaskStatus.Pending
        };

        task.RaiseDomainEvent(new TaskCreatedEvent(task));

        return task;
    }

    public void AddItem(string content)
    {
        _items.Add(new TaskItem(Id, content));
    }

    public void RemoveItem(int itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void UpdateStatus(Enums.TaskStatus status)
    {
        Status = status;
        if (status == Enums.TaskStatus.Completed)
        {
            CompletedAt = DateTime.UtcNow;
        }
        RaiseDomainEvent(new TaskUpdatedEvent(this));
    }

    public void Complete()
    {
        UpdateStatus(Enums.TaskStatus.Completed);
        RaiseDomainEvent(new TaskCompletedEvent(this));
    }

    public void UpdateDetails(TaskTitle title, TaskDescription description,
        Priority priority, DueDate dueDate)
    {
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;

        RaiseDomainEvent(new TaskUpdatedEvent(this));
    }

    public void AddComment(string author, string content)
    {
        _comments.Add(new Comment(Id, author, content));
    }

}
