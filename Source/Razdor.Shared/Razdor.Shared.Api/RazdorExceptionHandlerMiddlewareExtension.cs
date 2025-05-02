using Razdor.Shared.Api.ViewModels;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Api;

public static class RazdorExceptionHandlerMiddlewareExtension
{
    public static WebApplication UseRazdorExceptionHandlerMiddleware(this WebApplication app)
    {
        app.Use(HandleRazdorException);
        
        return app;
    }

    private static async Task HandleRazdorException(HttpContext context, Func<Task> next)
    {
        try
        {
            await next();
        }
        catch (RazdorException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ExceptionViewModel(
                exception.Code,
                exception.Message
            ));
        }
    }
}