using Mediator;
using Microsoft.AspNetCore.Mvc;
using Razdor.Identity.Module.Commands;
using Razdor.Identity.Module.Commands.ViewModels;

namespace Razdor.Identity.Api;

public static class IdentityRouter
{
    public static RouteGroupBuilder MapIdentityApiV0(this IEndpointRouteBuilder router)
    {
        var api = router.MapGroup("/auth").HasApiVersion(0.1);
        api.MapPost("/login", AuthAsync<LoginCommand>);
        api.MapPost("/signup", AuthAsync<SignupCommand>);
        return api;
    }
    
    private static async Task<IResult> AuthAsync<T>(
        [FromServices] IMediator mediator,
        [FromBody] T authCommand
    ) where T : ICommand<AuthenticationResult>
    {
        AuthenticationResult result = await mediator.Send(authCommand);
        if (result.TrySuccess(out var token, out var error))
        {
            return Results.Ok(token);
        }

        return Results.BadRequest(error);
    }
}