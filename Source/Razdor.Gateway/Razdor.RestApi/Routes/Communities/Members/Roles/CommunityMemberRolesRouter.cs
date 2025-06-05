using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Members.Commands;

namespace Razdor.RestApi.Routes.Communities.Members.Roles;

public static class CommunityMemberRolesRouter
{
    internal static IEndpointRouteBuilder MepCommunityMemberRoles(this IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder api = builder.MapGroup(
            "/communities/{communityId:ulong}/members/{userId:ulong}/roles/{roleId:ulong}"
        ).WithTags("Members", "Roles");

        api.MapPatch("/", ChangeMemberRolesAsync)
            .WithSummary("Добавить роль участнику сообщества");
        
        return builder;
    }
    
    private static async Task<IResult> ChangeMemberRolesAsync(
        [FromServices] ICommunitiesModule module,
        [FromRoute] ulong communityId,
        [FromRoute] ulong userId,
        [FromRoute] ulong roleId
    )
    {
        await module.ExecuteCommandAsync(
            new AddMemberRoleCommand(
                communityId, userId, roleId
            )    
        );

        return Results.Ok();
    }
}