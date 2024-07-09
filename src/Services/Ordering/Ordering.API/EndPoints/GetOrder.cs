using CommonOperations.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.EndPoints
{
  

    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
    public class GetOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));

                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);

            }).WithName("GetOrder")
                .Produces<GetOrdersResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Order")
                .WithDescription("Get Order");
        }
    }
}
