using Ordering.Application.Orders.Commands.CreateOrder;
namespace Ordering.API.EndPoints
{


    //-Mapps the  request to a CreateOrderCommand
    //Uses MeidatR to send the command to the Corresponding order
    //- Returns a response with the created order's ID 

    //-Accepts a CreateOrderRequest object
    public record CreateOrderRequest(OrderDto Order);   
    public record CreateOrderResponse(Guid Id);
  
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var comand = request.Adapt<CreateOrderCommand>();
                
                var results = await sender.Send(comand);

                var response = results.Adapt<CreateOrderResponse>();

                return Results.Created($"/orders/{response.Id}", response);
            })
                .WithName("CreateOrder")
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Order")
                .WithDescription("Create Order");
        }
    }
}
