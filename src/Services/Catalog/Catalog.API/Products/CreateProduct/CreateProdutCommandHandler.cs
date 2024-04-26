
namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category,string Description,string ImageFile,decimal Price) 
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandVaildator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandVaildator()
        {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
                RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
                RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("ImageFile is Required");
        }
    }
    internal class CreateProdutCommandHandler (
          IDocumentSession session, IValidator<CreateProductCommand> validator)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            var result = await validator.ValidateAsync(command, cancellationToken);
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            if (errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken); 
            
            return new CreateProductResult(product.Id);
        }
    }
}
