
using BuildingBlocks.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            var timeTaken = timer.Elapsed;

            logger.LogInformation("Handling request: {RequestType} - {@Request} - TotalTime {@timeTaken}", typeof(TRequest).Name, request,timeTaken);

            return response;
        }
    }
}
