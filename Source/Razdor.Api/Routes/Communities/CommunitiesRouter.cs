using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Razdor.Api.Routes.Communities.Channels;
using Razdor.Api.Routes.Communities.Invites;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.Commands;
using Razdor.Communities.Module.Services.Communities.Queries;
using Razdor.Communities.Module.Services.Communities.ViewModels;

namespace Razdor.Api.Routes.Communities;

public static class CommunitiesRouter
{
    public static IEndpointRouteBuilder MapCommunitiesApi(
        this IEndpointRouteBuilder builder,
        string pattern = "/"
    )
    {
        IEndpointRouteBuilder api = builder
            .NewVersionedApi("communities")
            .HasApiVersion(0.1)
            .MapGroup("/api/");

        api.MapInvites();
        api.MapCommunities();

        return builder;
    }
    
    private static IEndpointRouteBuilder MapCommunities(
        this IEndpointRouteBuilder builder
    )
    {
        RouteGroupBuilder api = builder.MapGroup("/communities")
            .RequireAuthorization();

        api.MapGet("/@my", GetSelfUserCommunitiesAsync)
            .Produces<IEnumerable<CommunityViewModel>>()
            .WithSummary("Получить список сообществ пользователя");

        api.MapGet("/{communityId:ulong}", GetCommunityAsync)
            .Produces<CommunityViewModel>()
            .WithSummary("Получить конкретное сообщество, но только из числа тех где состоит пользователь");

        api.MapPost("/", CreateCommunityAsync)
            .Produces<CommunityViewModel>()
            .WithSummary("Создать новое сообщество");

        api.MapCommunityInvites();
        api.MapCommunityChannels();

        return builder;
    }


    private static async Task<IResult> CreateCommunityAsync(
        [FromServices] ICommunityModule communityModule,
        [FromBody] CreateCommunityCommand command
    )
    {
        CommunityViewModel community = await communityModule.ExecuteCommandAsync(command);
        return Results.Ok(community);
    }

    private static async Task<IResult> GetSelfUserCommunitiesAsync(
        [FromServices] ICommunityModule communityModule
    )
    {
        IEnumerable<CommunityViewModel> communities = await communityModule.ExecuteQueryAsync(
            new GetSelfUserCommunitiesQuery()
        );

        return Results.Ok(communities);
    }

    private static async Task<IResult> GetCommunityAsync(
        [FromServices] ICommunityModule communityModule,
        [FromRoute] ulong communityId
    )
    {
        CommunityViewModel community = await communityModule.ExecuteQueryAsync(
            new GetCommunityQuery(communityId)
        );

        return Results.Ok(community);
    }
}