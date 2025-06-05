using Razdor.Api.ExceptionHandleMiddlewares.ViewModels;
using Razdor.Api.Serialization;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Extensions;

namespace Razdor.Api.ExceptionHandleMiddlewares;

public static class WebAppStatusCodeHandleMiddlewareExtensions
{
    public static WebApplication UseRazdorExceptionHandlerMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        return app;
    }

    public static WebApplication UseCustomNotAuthorizedResponse(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            await next(context);
            if (context.Response.StatusCode != StatusCodes.Status401Unauthorized)
                return;
            
            if (context.Response.Headers.IsReadOnly)
                return;
            
            await context.Response.WriteAsJsonAsync(
                new ExceptionViewModel(
                    ErrorCode.Unauthorized,
                    "Client is unauthorized."
                ),
                SharedJsonSerializerContext.Default.GetRequiredTypeInfo<ExceptionViewModel>()
            );
        });

        return app;
    }

    public static WebApplication UseNonExistentRouteResponse(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            await next(context);

            if (context.Response.StatusCode != StatusCodes.Status404NotFound)
                return;

            if (context.Response.Headers.IsReadOnly)
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