using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Shared.Infrastructure.EventDispatcher;
using Tasking.Infrastructure.Projections;

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
    public DbSet<Domain.ReadModels.TaskingUserRole> UserRoles => Set<Domain.ReadModels.TaskingUserRole>();
    public DbSet<TaskingUserProjection> TaskingUsers => Set<TaskingUserProjection>();
}
