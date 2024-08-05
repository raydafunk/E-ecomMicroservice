using Ordering.API;
using Ordering.Application;
using Ordering.Infrastruture;
using Ordering.Infrastruture.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// add services to the container 
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);


var app = builder.Build();

// Configure the http request pipeline 
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitailseDatabaseAsync();
}
app.Run();
