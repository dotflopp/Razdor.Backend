namespace Razdor.Communities.Api;

public static class EndpointRouterExtensions
{
    public static IEndpointRouteBuilder MapCommunitiesApi(
        this IEndpointRouteBuilder builder,
        string pattern = "/"
    )
    {
        return builder
            .NewVersionedApi("communities")
            .HasApiVersion(0.1);
    }
}