using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Api.Communities.ViewModels;
using Razdor.Communities.Services.Communities.Commands;
using Razdor.Communities.Services.Communities.Commands.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.UseCases.Invites.Commands;

namespace Razdor.Communities.Api.Communities;

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
}