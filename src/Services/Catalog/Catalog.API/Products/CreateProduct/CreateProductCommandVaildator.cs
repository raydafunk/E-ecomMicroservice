
namespace Catalog.API.Products.CreateProduct;

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
