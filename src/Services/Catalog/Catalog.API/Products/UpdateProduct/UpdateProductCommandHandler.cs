
namespace Catalog.API.Products.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
  : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool isSucesss);

public class UpdateProductCommandHandlerVaildator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandHandlerVaildator()
    {
        RuleFor(command => command.Id).NotEmpty().WithName("the Product  id is requered");

        RuleFor(command => command.Name)
               .NotEmpty().WithMessage("Name is required")
               .Length(2, 150).WithMessage("Name must be and 150 characters");

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("The Price must be greater then 0");
    }
}
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
