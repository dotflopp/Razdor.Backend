using Razdor.Shared.Api.ViewModels;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Api;

public static class WebApplicationExtension
{
    public static WebApplication UseRazdorExceptionHandlerMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        return app;
    }

    public static WebApplication UseCustomNotAuthorizedResponse(this WebApplication app)
    {
        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await next(context);
            if (context.Response.StatusCode != StatusCodes.Status401Unauthorized)
                return;
            
            await context.Response.WriteAsJsonAsync(
                new ExceptionViewModel(
                    ErrorCode.Unauthorized,
                    "The user is unauthorized."
                )
            );
        });
        
        return app;
    }

    public static WebApplication MapNonExistentRouteResponse(this WebApplication app)
    {
        app.Map(@"{**path}", (string? path = null) => Results.NotFound(
            new ExceptionViewModel(
                ErrorCode.NonExistentRoute,
                "Attempt to access not existing route"
            )
        ));
        return app;
    }
}