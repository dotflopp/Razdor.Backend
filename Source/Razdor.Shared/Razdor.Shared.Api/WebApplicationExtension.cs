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
}