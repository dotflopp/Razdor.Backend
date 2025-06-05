using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Invites.Commands;
using Razdor.Communities.Module.Services.Invites.Queries;
using Razdor.Communities.Module.Services.Invites.ViewModels;
using Razdor.RestApi.Routes.Invites.ViewModels;

namespace Razdor.RestApi.Routes.Invites;

public static class InvitesRouter
{
    public static IEndpointRouteBuilder MapCommunityInvites(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("/communities/{communityId:ulong}/invites")
            .WithTags("Communities", "Invites");

        api.MapPost("/", CreateInviteAsync)
            .Produces<InvitePreviewModel>()
            .WithSummary("Создать приглашение в сообщество");

        return builder;
    }


    public static IEndpointRouteBuilder MapInvites(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("/invites/{inviteId}")
            .WithTags("Invites");
        
        api.MapPost("/", AcceptInviteAsync)
            .WithSummary("Принять приглашение в сообщество")
            .Produces<InvitePreviewModel>();

        api.MapGet("/", GetInviteAsync)
            .WithSummary("Получить информацию о приглашении в сообщество")
            .Produces<InviteViewModel>();
            
        return builder;
    }
    
    private static async Task<IResult> GetInviteAsync(       
        [FromServices] ICommunitiesModule module,
        [FromRoute] string inviteId
    )
    {
        InviteViewModel invite = await module.ExecuteCommandAsync(
            new GetInviteCommand(inviteId)    
        );

        return Results.Ok(invite);
    }

    private static async Task<IResult> CreateInviteAsync(
        [FromServices] ICommunitiesModule module,
        [FromRoute] ulong communityId,
        [FromBody] InviteParametersViewModel parameters
    )
    {
        TimeSpan? lifeTime = null;
        if (parameters.LifeTime is { } lifeTimeSeconds)
            lifeTime = TimeSpan.FromSeconds(lifeTimeSeconds);

        InvitePreviewModel inviteId = await module.ExecuteCommandAsync(
            new CreateInviteCommand(communityId, lifeTime)
        );

        return Results.Ok(inviteId);
    }
    
    
    private static async Task<IResult> AcceptInviteAsync(
        [FromServices] ICommunitiesModule module,
        [FromRoute] string inviteId
    )
    {
        InvitePreviewModel model = await module.ExecuteCommandAsync(
            new AcceptInviteCommand(inviteId)
        );

        return Results.Ok(model);
    }
}