﻿using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.EndPoints
{

    public record UpdateOrderRequest(OrderDto OrderDto);
    public record UpdateOrderResponse(bool IsSucesss);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var comand = request.Adapt<UpdataOrderCommand>();

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
