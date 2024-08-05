using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.EndPoints
{


    public record DeleteOrderResponse(bool IsSucess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
            {
                var results = await sender.Send(new DeleteOrderCommand(Id));

                var response = results.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
                .WithName("Delete Order")
                .Produces<DeleteOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Order")
                .WithDescription("Delete Order");
        }
    }
}
