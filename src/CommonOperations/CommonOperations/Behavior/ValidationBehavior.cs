using CommonOperations.CQRS;
using FluentValidation;
using MediatR;

namespace CommonOperations.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TRequest>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failuers = validationResults
                          .Where(r => r.Errors.Any())
                          .SelectMany(r => r.Errors)
                          .ToList();

            if (failuers.Any())
                throw new ValidationException(failuers);

            return await next();    
        }
    }
}
