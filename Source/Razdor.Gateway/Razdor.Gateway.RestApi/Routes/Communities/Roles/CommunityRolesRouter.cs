using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Communities.Module.Services.Roles.Commands;
using Razdor.RestApi.Routes.Communities.Roles.ViewModels;

namespace Razdor.RestApi.Routes.Communities.Roles;

public static class CommunityRolesRouter
{
    internal static IEndpointRouteBuilder MapCommunityRoles(this IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder api = builder
            .MapGroup("communities/{communityId}/roles")
            .RequireAuthorization()
            .WithTags("Roles");

        api.MapPost("/", CreateRoleAsync)
            .Produces<RoleViewModel>()
            .WithSummary("Создать роль в сообществе");
        
        return builder;
    }
    
    private static async Task<IResult> CreateRoleAsync(
        [FromServices] ICommunitiesModule module,
        [FromRoute] ulong communityId,
        [FromBody] RolePyload rolePyload
    )
    {
        RoleViewModel role = await module.ExecuteCommandAsync(new CreateRoleCommand(
            communityId,
            rolePyload.Name,
            rolePyload.Permissions,
            rolePyload.Priority,
            rolePyload.IsMentionable,
            rolePyload.Color
        ));

        return Results.Ok(role);
    }
}