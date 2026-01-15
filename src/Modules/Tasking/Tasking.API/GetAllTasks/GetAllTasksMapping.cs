using Shared.Application.Results;
using Tasking.Application.Tasks.GetTasks;

namespace Tasking.API.GetAllTasks;

public static class GetAllTasksMapping
{
    public static Result<GetAllTasksResponse> ToGetAllTasksResponse(this Result<GetAllTasksResult> result)
    {
        if (result.IsFailure)
        {
            return Result<GetAllTasksResponse>.Failure(result.Error);
        }

        return Result<GetAllTasksResponse>.Success(new GetAllTasksResponse
        (
            result.Value!.TasksDto.Select(t => new TaskDto(
                Id: t.Id,
                Title: t.Title,
                Description: t.Description,
                Priority: t.Priority,
                Status: t.Status,
                DueDate: t.DueDate,
                Items: t.Items.Select(it => new TaskItemDto(
                    Id: it.Id,
                    Content: it.Content,
                    IsCompleted: it.IsCompleted)).ToList(),
                Comments: t.Comments.Select(c => new CommentDto(
                    Id: c.Id,
                    Content: c.Content,
                    Author: c.Author)).ToList()
                )).ToList()
        ));
    }
}
