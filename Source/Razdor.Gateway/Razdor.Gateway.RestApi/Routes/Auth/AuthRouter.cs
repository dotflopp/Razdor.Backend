using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Services.Auth.Commands;
using Razdor.Identity.Module.Services.Auth.Commands.ViewModels;
using Razdor.RestApi.ExceptionHandleMiddlewares.ViewModels;

namespace Razdor.RestApi.Routes.Auth;

public static class AuthRouter
{
    public static IEndpointRouteBuilder MapAuth(
        this IEndpointRouteBuilder router,
        [StringSyntax("Route")] string groupPrefix = "/auth"
    )
    {
        RouteGroupBuilder api = router.MapGroup(groupPrefix)
            .WithTags("Auth");

        api.MapPost("/login", AuthAsync<LoginCommand>)
            .Produces<AccessToken>()
            .Produces<ExceptionViewModel>(400)
            .WithSummary("Войти");
        api.MapPost("/signup", AuthAsync<SignupCommand>)
            .Produces<AccessToken>()
            .Produces<ExceptionViewModel>(400)
            .WithSummary("Зарегистрироваться");

        return api;
    }

    private static async Task<IResult> AuthAsync<T>(
        [FromServices] IIdentityModule module,
        [FromBody] T authCommand
    ) where T : IIdentityCommand<AccessToken>
    {
        AccessToken result = await module.ExecuteCommandAsync(authCommand);
        return Results.Ok(result);
    }
}