using CommonOperations.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;


namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderValidator: AbstractValidator<CreateOrderCommand>
{
	public CreateOrderValidator()
	{
		RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
		RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
		RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems is required");
	}
}