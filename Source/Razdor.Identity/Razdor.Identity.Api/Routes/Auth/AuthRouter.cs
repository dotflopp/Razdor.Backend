using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Razdor.Identity.Module.Auth.Commands;
using Razdor.Identity.Module.Contracts;

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

        api.MapPost("/login", AuthAsync<LoginCommand>);
        api.MapPost("/signup", AuthAsync<SignupCommand>);

        return api;
    }

    private static async Task<IResult> AuthAsync<T>(
        [FromServices] IIdentityModule module,
        [FromBody] T authCommand
    ) where T : IIdentityCommand<AuthenticationResult>
    {
        var result = await module.ExecuteCommandAsync(authCommand);
        if (result.TrySuccess(out var token, out var error)) return Results.Ok(token);

        return Results.BadRequest(error);
    }
}