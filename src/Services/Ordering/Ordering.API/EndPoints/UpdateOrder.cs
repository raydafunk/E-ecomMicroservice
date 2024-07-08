using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.EndPoints
{
    //-Accepts a UpdateOrderRequest
    //-Mapps the  request to a CreateOrderCommand
    //Uses MeidatR to send the command to the Corresponding order
    //- Returns a sucesss or  error reposne based on the outcome

    public record UpdateOrderRequest(OrderDto OrderDto);
    public record UpdateOrderResponse(bool IsSucesss);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var comand = request.Adapt<UpdateOrderResponse>();

                var results = await sender.Send(comand);

                var response = results.Adapt<UpdateOrderResponse>();

                return Results.Ok(response);

            })
                . WithName("UpdateOrder")
                .Produces<UpdateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Order")
                .WithDescription("Update Order");
        }
    }
}
