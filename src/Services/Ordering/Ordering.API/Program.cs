using Ordering.API;
using Ordering.Application;
using Ordering.Infrastruture;

var builder = WebApplication.CreateBuilder(args);

// add services to the container 
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();


var app = builder.Build();

// Configure the http request pipeline 

app.Run();
