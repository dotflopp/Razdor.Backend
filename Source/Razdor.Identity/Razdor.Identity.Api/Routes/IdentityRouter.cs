using Razdor.Identity.Api.Routes.Auth;
using Razdor.Identity.Api.Routes.Users;

namespace Razdor.Identity.Api.Routes;

public static class IdentityRouter
{
    public static IEndpointRouteBuilder MapIdentityApi(this IEndpointRouteBuilder router)
    {
        RouteGroupBuilder api = router
            .NewVersionedApi("identity")
            .HasApiVersion(0.1)
            .MapGroup("/api/");

        api.MapAuth();
        api.MapUsers();
        return api;
    }
}