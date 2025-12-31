using Microsoft.EntityFrameworkCore;
using Tasking.Application.Users;
using Tasking.Infrastructure.Database;

namespace Tasking.Infrastructure.Users;

public class TaskingUserReadRepository(
    TaskingDbContext context)
    : ITaskingUserReadRepository
{
    public async Task<IReadOnlyList<TaskingUserDto>> GetActiveUsers()
        => await context.TaskingUsers
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Select(x => new TaskingUserDto
            (
                UserId: x.UserId,
                DisplayName: x.DisplayName,
                Email: x.Email,
                IsActive: x.IsActive
            )).ToListAsync();

    public async Task<TaskingUserDto?> GetById(Guid userId) => 
        await context.TaskingUsers
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => new TaskingUserDto
            (
                UserId: x.UserId,
                DisplayName: x.DisplayName,
                Email: x.Email,
                IsActive: x.IsActive
            )).FirstOrDefaultAsync();
}
