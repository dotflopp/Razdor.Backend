using Razdor.Api.Routes.Identity.Auth;
using Razdor.Api.Routes.Identity.Users;

namespace Razdor.Api.Routes.Identity;

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