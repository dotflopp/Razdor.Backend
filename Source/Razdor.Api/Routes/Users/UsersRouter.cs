using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Razdor.Api.Middlewares.ViewModels;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Services.Users.Avatars.Queries;
using Razdor.Identity.Module.Services.Users.Commands;
using Razdor.Identity.Module.Users.Queries;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Messages.Module.Services.Commands.ViewModels;

namespace Razdor.Api.Routes.Users;

public static class UsersRouter
{
    internal static IEndpointRouteBuilder MapUsers(
        this IEndpointRouteBuilder router,
        [StringSyntax("Route")] string groupPrefix = "/users"
    )
    {
        RouteGroupBuilder api = router.MapGroup(groupPrefix)
            .WithTags("Users");

        api.MapGet("/@me", GetSelfUserAsync)
            .RequireAuthorization()
            .WithSummary("Получить данные аутентифицированного пользователя")
            .Produces<SelfUserViewModel>()
            .Produces((int)HttpStatusCode.Unauthorized)
            .Produces<ExceptionViewModel>((int)HttpStatusCode.NotFound);
        
        api.MapPost("/@me/avatar", UploadAvatarAsync)
            .RequireAuthorization()
            .DisableAntiforgery()
            .WithSummary("Установить аватар пользователя")
            .Produces((int)HttpStatusCode.Unauthorized)
            .Produces<ExceptionViewModel>((int)HttpStatusCode.NotFound);

        api.MapGet("/{userId:ulong}/avatar", GetUserAvatarAsync)
            .Produces<FileContentResult>();
        
        api.MapGet("/{userId:ulong}", GetUserAsync)
            .Produces<UserPreviewModel>()
            .Produces((int)HttpStatusCode.NotFound)
            .WithSummary("Получить данные пользователя с соответствующим идентификатором");
        
        api.MapPut("/@me/status", SetStatusAsync)
            .Produces((int)HttpStatusCode.NotFound)
            .WithSummary("Поменять статус пользователя");
        
        return router;
    }
    private static async Task<IResult> GetUserAvatarAsync(
        [FromServices] IIdentityModule module,
        [FromRoute] ulong userId
    ) 
    {
        MediaFile media = await module.ExecuteCommandAsync(
            new GetUserAvatarQuery(userId)
        );

        return Results.File(media.Stream, media.ContentType, media.FileName);
    }
    private static async Task UploadAvatarAsync(
        [FromServices] IIdentityModule module,
        [FromForm] IFormFile file
    )
    {
        await module.ExecuteCommandAsync(
            new UploadUserAvatarCommand(
                file.FileName,
                file.ContentType,
                file.OpenReadStream()
            )
        );
    }
    
    private static async Task<IResult> SetStatusAsync(
        [FromServices] IIdentityModule module,
        [FromBody] ChangeSelectedStatusCommand command
    )
    {
        await module.ExecuteCommandAsync(command);
        return Results.Ok();
    }

    private static async Task<IResult> GetUserAsync(
        [FromServices] IIdentityModule identity,
        [FromRoute] ulong userId
    )
    {
        UserPreviewModel user = await identity.ExecuteQueryAsync(new GetUserQuery(userId));
        return Results.Ok(user);
    }

    private static async Task<IResult> GetSelfUserAsync(
        [FromServices] IIdentityModule identity
    )
    {
        SelfUserViewModel user = await identity.ExecuteQueryAsync(new GetSelfUserQuery());
        return Results.Ok(user);
    }
}