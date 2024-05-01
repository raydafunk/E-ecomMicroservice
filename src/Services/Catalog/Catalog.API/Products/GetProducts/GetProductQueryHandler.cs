namespace Catalog.API.Products.GetProducts;
public record GetProductsQuery(int? Pagenumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.Pagenumber ?? 1, query.PageSize ?? 10,cancellationToken);
        return new GetProductsResult(products);
    }
}
