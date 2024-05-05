
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBasketRequest(string Username);
public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapDelete("/basket/{username}", async(string username, ISender sender) =>
       {
           var result = await sender.Send(new DeleteBasketCommand(username));

           var response = result.Adapt<DeleteBasketResponse>();

           return Results.Ok(response);
       })
            .WithName("DeleteProduct")
             .Produces<GetBasketResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithSummary(" Delete Product")
             .WithDescription(" Delete Product");
    }
}
