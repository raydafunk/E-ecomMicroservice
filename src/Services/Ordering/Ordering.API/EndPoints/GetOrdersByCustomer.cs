
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.EndPoints
{
    //-Accepts customer ID
    //-Uses a GetOrdersByCustomerQuery to fetch orders
    //Return the list of orders for that customners
    //

     public record GetOrderByNameResponse(IEnumerable<OrderDto> OrderDtos);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName,ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(orderName));

                var response = result.Adapt<GetOrderByNameResponse>();

                return Results.Ok(response);
            })
                .WithName("GetOrdersbyName")
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Order by Name")
                .WithDescription("Get Orders By Name"); ;
        }
    }
}
