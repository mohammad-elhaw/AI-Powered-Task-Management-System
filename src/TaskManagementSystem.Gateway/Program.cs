using TaskManagementSystem.Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
await app.AddApplicationAsync();
await app.RunAsync();