
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
    public record CheckoutBasketResponse(bool IsSuccess);

    public class CheckoutBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkeout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckoutBasketResponse>();

                return Results.Ok(response);
            })
             .WithName("CreateBasket")
              .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Check out Basket")
              .WithDescription("Checkout Basket");
        }
    }
}
