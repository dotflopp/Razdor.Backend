using System.Diagnostics;
using Mediator;
using Microsoft.Extensions.Logging;

namespace Razdor.Shared.Module;

public class PerfomanceLoggerBhavior<TMessage, TResponse>(
    ILogger<PerfomanceLoggerBhavior<TMessage, TResponse>> logger
): IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{

    public ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        try
        {
            return next(message, cancellationToken);
        }
        finally
        {
            stopwatch.Stop();
            logger.LogDebug($"{typeof(TMessage).Name} was processed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
