using Identity.API;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Events;
using Shared.Infrastructure.Extensions;
using Shared.Messaging;
using Tasking.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Tasking.API.ServiceCollectionExtension).Assembly)
    .AddApplicationPart(typeof(Identity.API.ServiceCollectionExtension).Assembly);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddTaskingAPI(builder.Configuration);
builder.Services.AddIdentityAPI(builder.Configuration);

builder.Services.AddMediatorAssemblies(
    typeof(Tasking.Application.ServiceCollectionExtension).Assembly,
    typeof(Identity.Application.ServiceCollectionExtension).Assembly);

builder.Services.AddCapMessaging(builder.Configuration);
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
