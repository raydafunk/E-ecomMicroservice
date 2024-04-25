

namespace Catalog.API.Products.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
  : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool isSucesss);
internal class UpdateProductCommandHandler
    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

        var products = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (products == null) 
        {
            throw new ProductNotFoundExpection();
        }
        products.Name = command.Name;
        products.Category = command.Category;
        products.Description = command.Description;
        products.ImageFile = command.ImageFile;
        products.Price = command.Price;

        session.Update(products);
        
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
