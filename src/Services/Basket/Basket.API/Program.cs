using Discount.Grpc.Protos;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using CommonOperations.Messaging.MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container 
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


// Data Services
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.Username);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(opitons =>
{
    opitons.Configuration = builder.Configuration.GetConnectionString("Redis");
});

//Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opitons =>
{
    opitons.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
 .ConfigurePrimaryHttpMessageHandler(() => // for development purpress not production
 {
     var handler = new HttpClientHandler
     {
         ServerCertificateCustomValidationCallback =
         HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
     };
     return handler;
 });

//Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);

//Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
       .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
       .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

//Configure the HTTP request pipeline 
app.MapCarter();
app.UseExceptionHandler(opitons => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
         ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
