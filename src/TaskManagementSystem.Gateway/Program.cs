using Identity.API;
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

builder.Services.AddSharedInfrastructure(
    typeof(Tasking.Application.ServiceCollectionExtension).Assembly,
    typeof(Identity.Application.ServiceCollectionExtension).Assembly);


var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
