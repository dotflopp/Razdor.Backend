using Razdor.Shared.Api.ViewModels;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Api;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
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
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, RazdorException exception)
    {
         
        //TODO надо верить в то что однажды появятся нормальные статус коды, по идентификатору которых можно будет понять их принадлежность
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        if (exception.Code.ToString().EndsWith("NotFound"))
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }

        ExceptionViewModel exceptionViewModel = new(
            exception.Code,
            exception.Message
        );
        _logger.LogError($"StatusCode: {context.Response.StatusCode} Exception: {exceptionViewModel}");        
        await context.Response.WriteAsJsonAsync(exceptionViewModel);
    }
}