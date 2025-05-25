using Razdor.Shared.Api.ViewModels;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Extensions;

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
                ),
                SharedJsonSerializerContext.Default.GetRequiredTypeInfo<ExceptionViewModel>()
            );
        });
        
        return app;
    }

    public static WebApplication UseNonExistentRouteResponse(this WebApplication app)
    {
        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await next(context);
            
            if (context.Response.StatusCode != StatusCodes.Status404NotFound)
                return;

            await context.Response.WriteAsJsonAsync(
                new ExceptionViewModel(
                    ErrorCode.NonExistentRoute,
                    "Attempt to access not existing route"
                ),
                SharedJsonSerializerContext.Default.GetRequiredTypeInfo<ExceptionViewModel>()
            );
        });
        return app;
    }
}