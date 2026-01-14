using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Security;
using Tasking.Application.Tasks.Security;
using Tasking.Application.Tasks.Security.Authorization.AssignTask;
using Tasking.Application.Tasks.Security.Authorization.CreateTask;

namespace Tasking.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTaskingApplication(this IServiceCollection services)
    {
        services.AddScoped<IAssignTaskPolicy, AssignTaskPolicy>();
        services.AddScoped<ICreateTaskPolicy, CreateTaskPolicy>();
        services.AddScoped<IPermissionCatalog, TaskPermissionCatalog>();
        return services;
    }
}
