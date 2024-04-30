
namespace Catalog.API.Products.DeleteProduct;

public class DeleteProductCommandVaildator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandVaildator()
    {
        RuleFor(x => x.Id).NotEmpty(). WithMessage(" The Product Id must be provide");
    }
}
