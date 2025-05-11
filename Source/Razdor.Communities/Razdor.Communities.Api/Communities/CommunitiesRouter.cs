using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Api.Communities.Channels;
using Razdor.Communities.Services.Communities.Commands;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.Contracts;

namespace Razdor.Communities.Api.Communities;

public static class CommunitiesRouter
{
    public static IEndpointRouteBuilder MapCommunities(
        this IEndpointRouteBuilder builder,
        [StringSyntax("Route")]  string prefix = "/communities"
    ){
         RouteGroupBuilder api = builder.MapGroup(prefix).RequireAuthorization();
         api.MapGet("/@my", GetSelfUserCommunitiesAsync);
         api.MapPost("/", CreateCommunityAsync);
         
         api.MapChannels();
         
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

    private static Task GetSelfUserCommunitiesAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }
}