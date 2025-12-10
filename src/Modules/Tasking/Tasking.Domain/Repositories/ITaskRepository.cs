namespace Tasking.Domain.Repositories;

public interface ITaskRepository
{
    Task<Aggregates.Task> GetTask(Guid taskId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Aggregates.Task>> GetAllTasks(CancellationToken cancellationToken);
    Task AddTask(Aggregates.Task task, CancellationToken cancellationToken);
    Task UpdateTask(Aggregates.Task task, CancellationToken cancellationToken);
    Task DeleteTask(Aggregates.Task task, CancellationToken cancellationToken);
    Task<int> SaveChanges(CancellationToken cancellationToken);
}
