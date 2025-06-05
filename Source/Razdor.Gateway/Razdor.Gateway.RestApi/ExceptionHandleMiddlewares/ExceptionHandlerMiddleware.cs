using Razdor.RestApi.ExceptionHandleMiddlewares.ViewModels;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Extensions;
using SharedJsonSerializerContext = Razdor.Api.Serialization.SharedJsonSerializerContext;

namespace Razdor.RestApi.ExceptionHandleMiddlewares;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RazdorException ex)
        {
            await HandleRazdorException(context, ex);
        }
        catch (BadHttpRequestException ex)
        {
            await HandleBadHttpRequestException(context, ex);
        }
    }
    
    private async Task HandleBadHttpRequestException(HttpContext context, BadHttpRequestException exception)
    {
        ExceptionViewModel exceptionViewModel = new(
            ErrorCode.BadRequest,
            exception.Message
        );
        _logger.LogTrace("BadRequest {0}", exception);
        
        await context.Response.WriteAsJsonAsync(
            exceptionViewModel,
            SharedJsonSerializerContext.Default.GetRequiredTypeInfo<ExceptionViewModel>()
        );
    }

    private async Task HandleRazdorException(HttpContext context, RazdorException exception)
    {
        context.Response.StatusCode = MapErrorCodeToStatusCode(exception.ErrorCode);
        
        ExceptionViewModel exceptionViewModel = new(
            exception.ErrorCode,
            exception.Message
        );

        _logger.LogDebug($"StatusCode {context.Response.StatusCode} Exception {exceptionViewModel}");
        
        await context.Response.WriteAsJsonAsync(
            exceptionViewModel,
            SharedJsonSerializerContext.Default.GetRequiredTypeInfo<ExceptionViewModel>()
        );
    }
    
    private int MapErrorCodeToStatusCode(ErrorCode code)
        => code switch
        {
            ErrorCode.NotFound => StatusCodes.Status404NotFound,
            ErrorCode.AccessForbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status400BadRequest
        };
}