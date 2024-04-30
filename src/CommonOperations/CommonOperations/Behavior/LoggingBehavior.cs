using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CommonOperations.Behavior;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] handle request={Request} - Response= {Response} - RequestData ={RequestData}",
             typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timetaken = timer.Elapsed;
        if (timetaken.Seconds > 3)//if the timer is more is greater then 3 seconds then log information
            logger.LogWarning("[PERFORMANCE] the request {Request} took {TimeTaken} seconds..",
                typeof(TRequest).Name, timetaken.Seconds);

        logger.LogInformation("[End] handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}
