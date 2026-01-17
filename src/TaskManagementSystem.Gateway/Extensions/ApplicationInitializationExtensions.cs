using Shared.Infrastructure.Seeding;

namespace TaskManagementSystem.Gateway.Extensions;

public static class ApplicationInitializationExtensions
{
    public static async Task<WebApplication> AddApplicationAsync(this WebApplication app)
    {
        if(app.Environment.IsDevelopment())
            app.UseSwaggerWithUI();

        await app.InitializeModulesAsync();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }

    private static async Task InitializeModulesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var scopedProvider = scope.ServiceProvider;
        var initializers = scopedProvider.GetServices<IModuleInitializer>();

        foreach (var module in initializers)
            await module.MigrateAsync(scopedProvider);

        foreach (var module in initializers)
            await module.SeedAsync(scopedProvider);
    }

    private static WebApplication UseSwaggerWithUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management System API v1");
        });
        return app;
    }
}
