using Mediator;
using Microsoft.AspNetCore.Mvc;
using Razdor.Identity.Api.Auth;
using Razdor.Identity.Module.Commands;
using Razdor.Identity.Module.Commands.ViewModels;

namespace Razdor.Identity.Api;

public static class IdentityRouter
{
    public static IEndpointRouteBuilder MapIdentityApi(this IEndpointRouteBuilder router)
    {
        var api = router.MapGroup("/")
            .WithApiVersionSet("Identity")
            .HasApiVersion(0.1);

        api.MapAuth();
        api.MapUsers();
        return api;
    }
}