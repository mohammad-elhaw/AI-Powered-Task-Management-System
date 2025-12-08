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
    public DateTime? DueDate { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; } = default!;

    private readonly List<Comment> _comments = [];
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();

    private Task() { }
    private Task(TaskTitle title, TaskDescription description, Priority priority, DateTime dueDate)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        Priority = priority;
        DueDate = dueDate;
        Status = Enums.TaskStatus.Pending;

        RaiseDomainEvent(new TaskCreatedEvent(Id));
    }
    
    public static Task Create(TaskTitle title, TaskDescription description,
        Priority priority, DateTime dueDate)
    {
        return new Task(title, description, priority, dueDate);
    }

    public void UpdateStatus(Enums.TaskStatus status)
    {
        Status = status;
        if (status == Enums.TaskStatus.Completed)
        {
            CompletedAt = DateTime.UtcNow;
        }
        RaiseDomainEvent(new TaskUpdatedEvent(Id));
    }

    public void Complete()
    {
        UpdateStatus(Enums.TaskStatus.Completed);
        RaiseDomainEvent(new TaskCompletedEvent(Id));
    }

    public void UpdateDetails(TaskTitle title, TaskDescription description,
        Priority priority, DateTime dueDate)
    {
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;

        RaiseDomainEvent(new TaskUpdatedEvent(Id));
    }

    public void AddComment(string author, string content)
    {
        _comments.Add(new Comment(Id, author, content));
    }

}
