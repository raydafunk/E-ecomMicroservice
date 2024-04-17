using MediatR;

namespace CommonOperations.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse: notnull
{

}
