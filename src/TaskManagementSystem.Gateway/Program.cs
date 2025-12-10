using Tasking.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTaskingAPI(builder.Configuration);
builder.Services.AddControllers()
    .AddApplicationPart(typeof(Tasking.API.ServiceCollectionExtension).Assembly);

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
