

using Discount.Grpc.Protos;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasektCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasektCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("UserName is required");
    }
}
public class StoreBasketCommandHandler 
    (IBasketRepository repository, 
    DiscountProtoService.DiscountProtoServiceClient discountProto):
    ICommandHandler<StoreBasektCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasektCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.Username);
    }

    private  async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        // communatcate with Discount.Grpc and calculate lastest price of the products

        foreach (var item in cart.items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}
