using Razdor.Api.Routes.ViewModels;
using Razdor.Api.Serialization;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Extensions;

namespace Razdor.Api;

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
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, RazdorException exception)
    {
        //TODO надо верить в то что однажды появятся нормальные статус коды, по идентификатору которых можно будет понять их принадлежность
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        if (exception.ErrorCode.ToString().EndsWith("NotFound"))
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        else if (exception.ErrorCode == ErrorCode.AccessForbidden)
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

        ExceptionViewModel exceptionViewModel = new(
            exception.ErrorCode,
            exception.Message
        );

        _logger.LogInformation($"StatusCode: {context.Response.StatusCode} Exception: {exceptionViewModel}");
        await context.Response.WriteAsJsonAsync(
            exceptionViewModel,
            SharedJsonSerializerContext.Default.GetRequiredTypeInfo<ExceptionViewModel>()
        );
    }
}