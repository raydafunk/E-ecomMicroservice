

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
public class StoreBasketCommandHandler (IBasketRepository repository):
    ICommandHandler<StoreBasektCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasektCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        //TODO: store basket in database(use Marten update- if exist = update, if not)
        //TODO: update cache

        await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.Username);
    }
}
