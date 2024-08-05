using Ordering.Application.Orders.Commands.UpdateOrder;
namespace Ordering.API.EndPoints
{

    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSucesss);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var comand = request.Adapt<UpdateOrderCommand>();

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
