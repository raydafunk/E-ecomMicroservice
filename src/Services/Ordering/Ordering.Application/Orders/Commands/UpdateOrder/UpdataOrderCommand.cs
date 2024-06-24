
using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdataOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSucess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdataOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Oder Name is required");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer  Name is required");
    }
}