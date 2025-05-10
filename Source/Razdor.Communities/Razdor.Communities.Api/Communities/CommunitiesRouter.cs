using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Api.Communities.Channels;

namespace Razdor.Communities.Api.Communities;

public static class CommunitiesRouter
{
    public static IEndpointRouteBuilder MapCommunities(
        this IEndpointRouteBuilder builder,
        [StringSyntax("Route")]  string prefix = "/communities"
    ){
         RouteGroupBuilder api = builder.MapGroup(prefix).RequireAuthorization();
         api.MapGet("/@my", GetSelfUserCommunitiesAsync);
         api.MapPost("/", CreateGuildAsync);
         
         api.MapChannels();

         
         return builder;
    }

    private static Task CreateGuildAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static Task GetSelfUserCommunitiesAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }
}