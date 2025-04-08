using Razdor.Communities.Api.Guilds;

namespace Razdor.Communities.Api;

public static class EndpointRouterExtensions
{
    public static IEndpointRouteBuilder MapRazdorApi(
        this IEndpointRouteBuilder builder,
        string pattern = "/"
    ) {
        return builder
            .NewVersionedApi("communities")
            .HasApiVersion(0.1)
            .MapGuilds();
    }
}