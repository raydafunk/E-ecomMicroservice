﻿namespace Ordering.Application.Orders.Queries.GetOrdersByName;
public class GetOrdersByNameQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        // get the order
        var orders = await dbContext.Orders
               .Include(o => o.OrderItems)
               .AsNoTracking()
               .Where(o => o.OrderName.Value.Contains(query.Name))
               .OrderBy(o => o.OrderName.Value)
               .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToOrderDtoList()); 
    }
}
