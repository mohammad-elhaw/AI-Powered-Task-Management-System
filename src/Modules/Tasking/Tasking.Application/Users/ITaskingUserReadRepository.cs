namespace Tasking.Application.Users;

public interface ITaskingUserReadRepository
{
    Task<TaskingUserDto?> GetById(Guid userId);
    Task<IReadOnlyList<TaskingUserDto>> GetActiveUsers();
}