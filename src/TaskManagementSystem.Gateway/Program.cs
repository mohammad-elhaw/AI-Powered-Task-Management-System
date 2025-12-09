using Tasking.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTaskingAPI(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
