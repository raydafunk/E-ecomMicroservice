
namespace Catalog.API.Products.GetProductsByCategory;
public record GetProductByCategoryQuery(string Category) : IQuery<GetByProductByCategoryResult>;

public record GetByProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
   : IQueryHandler<GetProductByCategoryQuery, GetByProductByCategoryResult>
{
    public async Task<GetByProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsByCatagoryQueryHandler.handle called with {@Query}", query);
        
        var produts = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return  new GetByProductByCategoryResult(produts);
    }
}
