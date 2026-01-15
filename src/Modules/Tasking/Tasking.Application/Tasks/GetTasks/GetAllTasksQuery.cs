using Shared.Application.Abstractions.CQRS;

namespace Tasking.Application.Tasks.GetTasks;

public record GetAllTasksQuery : IQuery<GetAllTasksResult>;