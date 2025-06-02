using Razdor.Api.Routes.Auth;
using Razdor.Api.Routes.Users;

namespace Razdor.Api.Routes;

public static class ApiRouter
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