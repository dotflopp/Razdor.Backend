using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Api.Communities.ViewModels;
using Razdor.Communities.Services.Communities.Commands;
using Razdor.Communities.Services.Communities.Commands.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.UseCases.Invites.Commands;

namespace Razdor.Communities.Api.Invites;

public static class InvitesRouter
{
    public static IEndpointRouteBuilder MapInvites(this IEndpointRouteBuilder builder)
    { 
        builder.MapPost("/invites/{inviteId:alpha}", AcceptInviteAsync)
            .WithSummary("Принять приглашение в сообщество");
        
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
}