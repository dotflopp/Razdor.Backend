using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Users.Queries;
using Razdor.Identity.Module.Users.ViewModels;

namespace Razdor.Identity.Api.Routes.Users;

public static class UsersRouter
{
    internal static IEndpointRouteBuilder MapUsers(
        this IEndpointRouteBuilder router,
        [StringSyntax("Route")] string groupPrefix = "/users"
    ){
        RouteGroupBuilder api = router.MapGroup(groupPrefix)
            .WithTags("Users");

        api.MapGet("/@me", GetSelfUserAsync)
            .RequireAuthorization()
            .WithSummary("Вернет аутентифицированного пользователя");

        api.MapGet("/{userId:ulong}", GetUserAsync)
            .WithSummary("Вернет пользователя с соответствующим идентификатором");
        
        return router;
    }

    private static async Task<IResult> GetUserAsync(
        [FromServices] IIdentityModule identity,
        [FromRoute] ulong userId
    ){
        UserPreviewModel user = await identity.ExecuteQueryAsync(new GetUserQuery(userId));
        return Results.Ok(user);
    }

    private static async Task<IResult> GetSelfUserAsync(
        [FromServices] IIdentityModule identity
    ){
        SelfUserViewModel user = await identity.ExecuteQueryAsync(new GetSelfUserQuery());   
        return Results.Ok(user);
    }
}