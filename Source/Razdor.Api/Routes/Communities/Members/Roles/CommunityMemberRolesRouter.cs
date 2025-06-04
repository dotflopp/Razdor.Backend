using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Members.Commands;
using Razdor.Communities.Module.Services.Members.ViewModels;

namespace Razdor.Api.Routes.Communities.Members.Roles;

public static class CommunityMemberRolesRouter
{
    internal static IEndpointRouteBuilder MepCommunityMemberRoles(this IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder api = builder.MapGroup(
            "/communities/{communityId:ulong}/members/{userId:ulong}/roles"
        );

        api.MapPost("/", ChangeMemberRolesAsync);
        
        return builder;
    }
    
    private static async Task<IResult> ChangeMemberRolesAsync(
        [FromServices] ICommunitiesModule module,
        [FromRoute] ulong communityId,
        [FromRoute] ulong userId,
        [FromBody] MemberRolesViewModel member
    )
    {
        await module.ExecuteCommandAsync(
            new ChangeMemberRolesCommand(
                communityId, userId, member.Roles.Select(x => ulong.Parse(x)).ToList()
            )    
        );

        return Results.Ok();
    }
}