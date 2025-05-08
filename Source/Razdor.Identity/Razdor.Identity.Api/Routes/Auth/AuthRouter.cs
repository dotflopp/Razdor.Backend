using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Razdor.Identity.Module.Auth.Commands;
using Razdor.Identity.Module.Auth.Commands.ViewModels;
using Razdor.Identity.Module.Contracts;
using Razdor.Shared.Api.ViewModels;

namespace Razdor.Identity.Api.Routes.Auth;

public static class AuthRouter
{
    public static IEndpointRouteBuilder MapAuth(
        this IEndpointRouteBuilder router,
        [StringSyntax("Route")] string groupPrefix = "/auth"
    )
    {
        var api = router.MapGroup(groupPrefix)
            .WithTags("Auth");

        api.MapPost("/login", AuthAsync<LoginCommand>)
            .Produces<AccessToken>(200)
            .Produces<ExceptionViewModel>(400);
        api.MapPost("/signup", AuthAsync<SignupCommand>)
            .Produces<AccessToken>(200)
            .Produces<ExceptionViewModel>(400);
        
        return api;
    }

    private static async Task<IResult> AuthAsync<T>(
        [FromServices] IIdentityModule module,
        [FromBody] T authCommand
    ) where T : IIdentityCommand<AccessToken>
    {
        var result = await module.ExecuteCommandAsync(authCommand);
        return Results.Ok(result);
    }
}