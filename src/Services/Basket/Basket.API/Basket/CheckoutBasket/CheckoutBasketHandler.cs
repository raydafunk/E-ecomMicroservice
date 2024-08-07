
using CommonOperations.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandVaildator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandVaildator()
        {
                RuleFor( x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckdto cannot be not null");
                RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("Username is required");
        }
    }
    public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) 
                : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
           // get the existing Basket with total price 

            var  userbasket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            if (userbasket == null)
            {
                return new CheckoutBasketResult(false);
            }

            var totalSumEventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();

            totalSumEventMessage.TotalPrice = userbasket.TotalPrice;

            await publishEndpoint.Publish(totalSumEventMessage, cancellationToken);

            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);  
        }
    }
}
