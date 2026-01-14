using Identity.Application.Roles;
using Identity.Domain.Aggregates;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Security;
using Shared.Infrastructure.Seeding;

namespace Identity.Infrastructure.Startup;

public class RoleSeeder(
    IRoleRepository roleRepository,
    DbContext context,
    IEnumerable<IPermissionCatalog> catalogs
    ) : IDataSeeder
{
    public async Task Seed(CancellationToken cancellationToken)
    {
        var allPermissionCodes = catalogs
            .SelectMany(c => c.AllPermissions())
            .ToHashSet();

        var allPermissions = await EnsurePermissions(allPermissionCodes, cancellationToken);

        await EnsureAdmin(allPermissions, cancellationToken);
        await EnsureUser(allPermissions, cancellationToken);
    }

    private async Task<List<Permission>> EnsurePermissions(HashSet<string> codes, 
        CancellationToken cancellationToken)
    {
        var existing = await context.Set<Permission>()
            .Where(p => codes.Contains(p.Code))
            .ToListAsync(cancellationToken);

        var existingCodes = existing.Select(p => p.Code).ToHashSet();

        var toAdd = codes
            .Where(code => !existingCodes.Contains(code))
            .Select(code => new Permission(code, code))
            .ToList();

        if(toAdd.Count > 0)
        {
            await context.Set<Permission>().AddRangeAsync(toAdd, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        return await context.Set<Permission>()
        .Where(p => codes.Contains(p.Code))
        .ToListAsync(cancellationToken);
    }


    private async Task EnsureAdmin(List<Permission> allPermissions, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByName(DefaultRoles.Admin, cancellationToken)
            ?? Role.Create(Guid.NewGuid(), DefaultRoles.Admin);

        allPermissions.Where(p => !role.Permissions.Any(rp => rp.PermissionId == p.Id))
            .ToList()
            .ForEach(p => role.AddPermission(p));

        await roleRepository.SaveChanges(cancellationToken);
    }

    private async Task EnsureUser(List<Permission> allPermissions, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByName(DefaultRoles.User, cancellationToken)
                   ?? Role.Create(Guid.NewGuid(), DefaultRoles.User);

        var userPermissions = allPermissions
            .Where(p => p.Code == "tasks.view" || p.Code == "tasks.create");

        userPermissions.Where(p => !role.Permissions.Any(rp => rp.PermissionId == p.Id))
            .ToList()
            .ForEach(p => role.AddPermission(p));

        await roleRepository.SaveChanges(cancellationToken);
    }
}
