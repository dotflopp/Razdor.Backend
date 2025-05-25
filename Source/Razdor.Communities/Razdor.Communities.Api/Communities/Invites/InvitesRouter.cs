using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Api.Communities.ViewModels;
using Razdor.Communities.Services.Communities.Commands;
using Razdor.Communities.Services.Communities.Commands.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.UseCases.Invites.Commands;

namespace Razdor.Communities.Api.Communities;

public static class InvitesRouter
{
    public static IEndpointRouteBuilder MapChannels(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("{communityId:ulong}/invites");

        api.MapPost("/", CreateInviteAsync);
        api.MapPost("/{inviteId:alpha}", AcceptInviteAsync);
        
        return builder;
    }

    private static async Task<IResult> AcceptInviteAsync(
        [FromServices] ICommunityModule module,
        [FromRoute] string inviteId
    ){
        await module.ExecuteCommandAsync(
            new AcceptInviteCommand(inviteId)
        );

        return Results.Ok();
    }

    private static async Task<IResult> CreateInviteAsync(
        [FromServices] ICommunityModule module,
        [FromRoute] ulong communityId,
        [FromBody] InviteParametersViewModel parameters
    )
    {
        InviteViewModel invite = await module.ExecuteCommandAsync(
            new CreateInviteCommand(communityId, parameters.LifeTime)
        );

        return Results.Ok(invite);
    }
}