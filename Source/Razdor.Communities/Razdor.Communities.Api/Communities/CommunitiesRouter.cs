using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Api.Communities.Channels;
using Razdor.Communities.Services.Communities.Commands;
using Razdor.Communities.Services.Communities.Queries;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Communities.Queries;

namespace Razdor.Communities.Api.Communities;

public static class CommunitiesRouter
{
    public static IEndpointRouteBuilder MapCommunities(
        this IEndpointRouteBuilder builder,
        [StringSyntax("Route")] string prefix = "/communities"
    ){
         RouteGroupBuilder api = builder.MapGroup(prefix).RequireAuthorization();
         
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
    ){
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