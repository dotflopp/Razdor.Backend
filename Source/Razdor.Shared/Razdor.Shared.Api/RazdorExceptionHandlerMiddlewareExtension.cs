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
            ILogger logger = context.RequestServices.GetRequiredService<ILogger>();
            logger.LogError(exception.Message);        
         
            //TODO надо верить в то что однажды появятся нормальные статус коды, по идентификатору которых можно будет понять их принадлежность
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            if (exception.Code.ToString().EndsWith("NotFound"))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            
            await context.Response.WriteAsJsonAsync(new ExceptionViewModel(
                exception.Code,
                exception.Message
            ));
        }
    }
}