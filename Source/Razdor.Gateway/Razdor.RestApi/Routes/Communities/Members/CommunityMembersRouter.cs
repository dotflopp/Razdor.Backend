﻿using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Members.Queries;
using Razdor.Communities.PublicEvents.ViewModels.Members;

namespace Razdor.RestApi.Routes.Communities.Members;

public static class CommunityMembersRouter
{
    internal static IEndpointRouteBuilder MapCommunityMembers(
        this IEndpointRouteBuilder builder
    )
    {
        IEndpointRouteBuilder api = builder
            .MapGroup("/communities/{communityId:ulong}/members")
            .WithTags("Members");

        api.MapGet("/", GetCommunityMembers)
            .Produces<IEnumerable<CommunityMemberPreviewModel>>()
            .WithSummary("Получить пользователей сообщества");
        
        api.MapGet("/{userId:ulong}", GetCommunityMember)
            .Produces<CommunityMemberPreviewModel>()
            .WithSummary("Получить пользователя сообщества");
        
        return builder;
    }
    private static async Task<IResult> GetCommunityMember(
        [FromServices] ICommunitiesModule module,
        [FromRoute] ulong communityId,
        [FromRoute] ulong userId
    )
    {
        CommunityMemberPreviewModel member = await module.ExecuteQueryAsync(new GetCommunityMemberQuery(
            communityId,
            userId
        ));

        return Results.Ok(member);
    }
    private static async Task<IResult> GetCommunityMembers(       
        [FromServices] ICommunitiesModule module,
        [FromRoute] ulong communityId,
        [FromQuery] ulong? lastUserId,
        [FromQuery] int? usersCount
    )
    { 
        IEnumerable<CommunityMemberPreviewModel> members = await module.ExecuteQueryAsync(
            new GetCommunityMembersQuery(
                communityId,
                lastUserId,
                usersCount
            )
        );

        return Results.Ok(members);
    }
}