using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Shared.Infrastructure.Events;

namespace Tasking.Infrastructure.Database;

public class TaskingDbContext(
    DbContextOptions<TaskingDbContext> options,
    IDomainEventDispatcher dispatcher) 
    : ModuleDbContext(options, dispatcher)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tasking");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TaskingDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Domain.Aggregates.Task> Tasks => Set<Domain.Aggregates.Task>();
}
