using FluentValidation;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;   

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderValidator()
    {
        RuleFor(r => r.OrderId).NotEmpty().WithMessage("the OrderId is Requried");
    }
}