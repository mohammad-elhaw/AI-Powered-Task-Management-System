using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions;
using Shared.Infrastructure.Events;

namespace Shared.Infrastructure;

public class ModuleDbContext(DbContextOptions options,
    IDomainEventDispatcher dispatcher) : DbContext(options)
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        //we need to pass transaction to cap 
        using var transaction = await Database.BeginTransactionAsync(cancellationToken);

        var domainEvents = ChangeTracker
            .Entries<AggregateRoot<Guid>>()
            .Select(e => e.Entity)
            .SelectMany(e => e.DomainEvents)
            .ToList();

        foreach(var entry in ChangeTracker.Entries<AggregateRoot<Guid>>())
        {
            entry.Entity.ClearDomainEvents();
        }

        if (domainEvents.Count != 0 && dispatcher != null)
            await dispatcher.Dispatch(domainEvents);    
        
        var modifiedRowCount =await base.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return modifiedRowCount;
       
    }
}
