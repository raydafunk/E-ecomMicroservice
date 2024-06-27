﻿
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersbyCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
                           .Include(o => o.OrderItems)
                           .AsNoTracking()
                           .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                           .OrderBy(o => o.OrderName.Value)
                           .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}
