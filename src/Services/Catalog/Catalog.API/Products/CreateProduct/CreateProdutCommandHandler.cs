using Catalog.API.Models;
using CommonOperations.CQRS;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category,string Description,string ImageFile,decimal Price) 
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProdutCommandHandler : 
        ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
   
            // save the database 
            // return the product result 

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            //Todo
            // save the database 
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
