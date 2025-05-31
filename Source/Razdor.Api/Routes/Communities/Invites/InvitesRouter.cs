using Microsoft.AspNetCore.Mvc;
using Razdor.Api.Routes.Communities.Invites.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Communities.ViewModels;
using Razdor.Communities.Services.Services.Invites.Commands;

namespace Razdor.Api.Routes.Communities.Invites;

public static class InvitesRouter
{
    public static IEndpointRouteBuilder MapCommunityInvites(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("{communityId:ulong}/invites");

        api.MapPost("/", CreateInviteAsync)
            .Produces<InviteViewModel>()
            .WithSummary("Создать приглашение в сообщество");

        return builder;
    }
    
    public static IEndpointRouteBuilder MapInvites(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/invites/{inviteId:alpha}", AcceptInviteAsync)
            .WithSummary("Принять приглашение в сообщество")
            .Produces(StatusCodes.Status200OK);

        return builder;
    }

    private static async Task<IResult> CreateInviteAsync(
        [FromServices] ICommunityModule module,
        [FromRoute] ulong communityId,
        [FromBody] InviteParametersViewModel parameters
    )
    {
        TimeSpan? lifeTime = null;
        if (parameters.LifeTime is { } lifeTimeSeconds)
            lifeTime = TimeSpan.FromSeconds(lifeTimeSeconds);

        InviteViewModel invite = await module.ExecuteCommandAsync(
            new CreateInviteCommand(communityId, lifeTime)
        );

        return Results.Ok(invite);
    }
    
    
    private static async Task<IResult> AcceptInviteAsync(
        [FromServices] ICommunityModule module,
        [FromRoute] string inviteId
    )
    {
        await module.ExecuteCommandAsync(
            new AcceptInviteCommand(inviteId)
        );

        return Results.Ok();
    }
}