using Identity.API;
using Tasking.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Tasking.API.ServiceCollectionExtension).Assembly)
    .AddApplicationPart(typeof(Identity.API.ServiceCollectionExtension).Assembly);

builder.Services.AddTaskingAPI(builder.Configuration);
builder.Services.AddIdentityAPI(builder.Configuration);

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
