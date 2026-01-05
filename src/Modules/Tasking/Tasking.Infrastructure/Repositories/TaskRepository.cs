using Microsoft.EntityFrameworkCore;
using Tasking.Domain.Repositories;
using Tasking.Infrastructure.Database;

namespace Tasking.Infrastructure.Repositories;

public class TaskRepository(TaskingDbContext context) : ITaskRepository
{
    public async Task AddTask(Domain.Aggregates.Task task, CancellationToken cancellationToken) =>
        await context.Tasks.AddAsync(task, cancellationToken);
    

    public Task DeleteTask(Domain.Aggregates.Task task, CancellationToken cancellationToken)
    {
        context.Tasks.Remove(task);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Domain.Aggregates.Task>> GetAllTasks(CancellationToken cancellationToken)
        => await context.Tasks
            .Include(t => t.Items)
            .Include(t => t.Comments)
            .OrderByDescending(t => t.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);


    public async Task<Domain.Aggregates.Task> GetTask(Guid taskId, CancellationToken cancellationToken)
        => await context.Tasks
            .Include(t => t.Items)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
    // we will return result object directly if null

    public Task<int> SaveChanges(CancellationToken cancellationToken)
        => context.SaveChangesAsync(cancellationToken);

    public Task UpdateTask(Domain.Aggregates.Task task, CancellationToken cancellationToken)
    {
        context.Tasks.Update(task);
        return Task.CompletedTask;
    }
}