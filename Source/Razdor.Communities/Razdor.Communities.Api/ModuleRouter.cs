using Razdor.Communities.Api.Communities;
using Razdor.Communities.Api.Invites;

namespace Razdor.Communities.Api;

public static class ModuleRouter
{
    public static IEndpointRouteBuilder MapCommunitiesApi(
        this IEndpointRouteBuilder builder,
        string pattern = "/"
    )
    {
        IEndpointRouteBuilder api = builder
            .NewVersionedApi("communities")
            .HasApiVersion(0.1);
        
        api.MapInvites();
        api.MapCommunities();
        
        return builder;
    }
}