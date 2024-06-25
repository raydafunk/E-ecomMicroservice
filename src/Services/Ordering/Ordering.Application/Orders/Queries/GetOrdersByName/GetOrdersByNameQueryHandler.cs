﻿namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        // get the order
        var orders = await dbContext.Ord    ers
               .Include(o => o.OrderItems)
               .AsNoTracking()
               .Where(o => o.OrderName.Value.Contains(query.Name))
               .OrderBy(o => o.OrderName)
               .ToListAsync();

        var orderDtos = ProjectToOrdersDto(orders);


        return new GetOrdersByNameResult(orderDtos); 
    }

    private List<OrderDto> ProjectToOrdersDto(List<Order> orders) 
    {
        List<OrderDto> result = new();
        foreach (var order in orders)
        {
            var orderDto = new OrderDto(

                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                     order.ShippingAddress.FirstName,
                     order.ShippingAddress.LastName,
                     order.ShippingAddress.EmailAddress,
                     order.ShippingAddress.AddressLine,
                     order.ShippingAddress.Country,
                     order.ShippingAddress.State,
                     order.ShippingAddress.ZipCode),
                BillingAddress: new AddressDto(
                     order.BillingAddress.FirstName,
                     order.BillingAddress.LastName,
                     order.BillingAddress.EmailAddress,
                     order.BillingAddress.AddressLine,
                     order.BillingAddress.Country,
                     order.BillingAddress.State,
                     order.BillingAddress.ZipCode),
                Payment: new PaymentDto(
                    order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    order.Payment.PaymentMethod),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()

            );

            result.Add(orderDto);
        }
        return result;
    }
}
