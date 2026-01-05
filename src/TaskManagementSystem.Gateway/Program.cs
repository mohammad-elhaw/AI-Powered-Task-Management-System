using Identity.API;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Notifications.Infrastructure;
using Shared.Infrastructure;
using Tasking.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Tasking.API.ServiceCollectionExtension).Assembly)
    .AddApplicationPart(typeof(Identity.API.ServiceCollectionExtension).Assembly)
        .ConfigureApiBehaviorOptions(opts =>
        {
            opts.SuppressModelStateInvalidFilter = true;
        });

builder.Services.AddTaskingAPI(builder.Configuration);
builder.Services.AddIdentityAPI(builder.Configuration);
builder.Services.AddNotificationsInfrastructure(builder.Configuration);

builder.Services.AddSharedInfrastructure(
    typeof(Tasking.Application.ServiceCollectionExtension).Assembly,
    typeof(Identity.Application.ServiceCollectionExtension).Assembly);


var app = builder.Build();

var adp = app.Services.GetService<IActionDescriptorCollectionProvider>();
if (adp != null)
{
    foreach (var ad in adp.ActionDescriptors.Items)
    {
        var route = ad.AttributeRouteInfo?.Template ?? "(no route)";
        var controller = ad.RouteValues.TryGetValue("controller", out var c) ? c : "(no controller)";
        var action = ad.RouteValues.TryGetValue("action", out var a) ? a : "(no action)";
        Console.WriteLine($"Route: {route}  --> {controller}.{action}");
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
