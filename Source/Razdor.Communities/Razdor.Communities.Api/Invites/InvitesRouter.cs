using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Invites.Commands;

namespace Razdor.Communities.Api.Invites;

public static class InvitesRouter
{
    public static IEndpointRouteBuilder MapInvites(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/invites/{inviteId:alpha}", AcceptInviteAsync)
            .WithSummary("Принять приглашение в сообщество")
            .Produces(StatusCodes.Status200OK);

        return builder;
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