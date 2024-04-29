using CommonOperations.Execeptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommonOperations.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {execeptionMessage}, Time occurence {time}", exception.Message, DateTime.UtcNow);

            (string Detail, string Ttile, int StatusCode) details = exception switch
            {
                InternalServerExpection =>
                (
                   exception.Message,
                   exception.GetType().Name,
                   context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                   exception.Message,
                   exception.GetType().Name,
                   context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),

                BadRequestExpection =>
                (
                exception.Message,
                exception.GetType().Name,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),

                NotFoundExecption =>
                (exception.Message,
                  exception.GetType().Name,
                  context.Response.StatusCode = StatusCodes.Status404NotFound
                 ),
                _ =>
                (
                 exception.Message,
                  exception.GetType().Name,
                  context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };
            var problemDetails = new ProblemDetails
            {
                Title = details.Ttile,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("trackId", context.TraceIdentifier);

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }
            await context.Response.WriteAsJsonAsync( problemDetails, cancellationToken: cancellationToken );
            return true;
        }
    }
}
