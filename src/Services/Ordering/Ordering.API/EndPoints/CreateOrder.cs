using Ordering.Application.Orders.Commands.CreateOrder;
namespace Ordering.API.EndPoints
{
    public record CreateOrderRequest(OrderDto Order);   
    public record CreateOrderResponse(Guid Id);
  
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
          {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                
                var results = await sender.Send(command);

                var response = results.Adapt<CreateOrderResponse>();

                return Results.Created($"/orders/{response.Id}", response);
            })
                .WithName("Create Order")
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Order")
                .WithDescription("Create Order");
        }
    }
}
