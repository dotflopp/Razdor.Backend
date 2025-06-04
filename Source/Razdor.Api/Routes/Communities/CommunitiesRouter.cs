using Microsoft.AspNetCore.Mvc;
using Razdor.Api.Routes.Communities.ViewModels;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Communities.Module.Services.Channels.Queries;
using Razdor.Communities.Module.Services.Channels.ViewModels;
using Razdor.Communities.Module.Services.Communities.Commands;
using Razdor.Communities.Module.Services.Communities.InternalQueries;
using Razdor.Communities.Module.Services.Communities.Queries;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Messages.Module.Services.Commands.ViewModels;

namespace Razdor.Api.Routes.Communities;

public static class CommunitiesRouter
{
    public static IEndpointRouteBuilder MapCommunities(
        this IEndpointRouteBuilder builder
    )
    {
        RouteGroupBuilder api = builder.MapGroup("/communities")
            .RequireAuthorization();

        api.MapGet("/@my", GetSelfUserCommunitiesAsync)
            .Produces<IEnumerable<CommunityViewModel>>()
            .WithSummary("Получить список сообществ пользователя");

        api.MapGet("/{communityId:ulong}", GetCommunityAsync)
            .Produces<CommunityViewModel>()
            .WithSummary("Получить конкретное сообщество, но только из числа тех где состоит пользователь");

        api.MapPost("/", CreateCommunityAsync)
            .Produces<CommunityViewModel>()
            .WithSummary("Создать новое сообщество");
        
        api.MapGet("/{communityId:ulong}/channels", GetCommunityChannelsAsync)
            .Produces<IEnumerable<ChannelViewModel>>()
            .WithSummary("Получить каналы сообщества");
        
        api.MapPost("/{communityId:ulong}/channels", CreateCommunityChannelAsync)
            .Produces<ChannelViewModel>()
            .WithSummary("Создать новый канал в сообществе");
        
        api.MapPost("/{communityId:ulong}/avatar", UploadCommunityAvatarAsync)
            .WithSummary("Изменить аватар сообщества");
        
        api.MapGet("/{communityId:ulong}/avatar", GetCommunityAvatarAsync)
            .Produces<FileContentResult>()
            .WithSummary("Получить аватар сообщества");
        
        return builder;
    }
    private static async Task<IResult> GetCommunityAvatarAsync(
        [FromServices] ICommunityModule communityModule,
        [FromRoute] ulong communityId
    )
    {
        MediaFile file = await communityModule.ExecuteQueryAsync(new GetCommunityAvatarQuery(communityId));
        return Results.File(file.Stream, file.ContentType, file.FileName);
    }
    private static async Task UploadCommunityAvatarAsync(
        [FromServices] ICommunityModule communityModule,
        [FromRoute] ulong communityId,
        [FromForm] IFormFile file
    )
    {
        await communityModule.ExecuteCommandAsync(
            new UploadCommunityAvatarCommand(
                communityId,
                file.FileName,
                file.ContentType,
                file.OpenReadStream()
            )
        );
    }


    private static async Task<IResult> CreateCommunityAsync(
        [FromServices] ICommunityModule communityModule,
        [FromBody] CreateCommunityCommand command
    )
    {
        CommunityViewModel community = await communityModule.ExecuteCommandAsync(command);
        return Results.Ok(community);
    }

    private static async Task<IResult> GetSelfUserCommunitiesAsync(
        [FromServices] ICommunityModule communityModule
    )
    {
        IEnumerable<CommunityViewModel> communities = await communityModule.ExecuteQueryAsync(
            new GetSelfUserCommunitiesQuery()
        );

        return Results.Ok(communities);
    }

    private static async Task<IResult> GetCommunityAsync(
        [FromServices] ICommunityModule communityModule,
        [FromRoute] ulong communityId
    )
    {
        CommunityViewModel community = await communityModule.ExecuteQueryAsync(
            new GetCommunityQuery(communityId)
        );

        return Results.Ok(community);
    }
    
    
    private static async Task<IResult> CreateCommunityChannelAsync(
        [FromServices] ICommunityModule module,
        [FromRoute] ulong communityId,
        [FromBody] CommunityChannelConfiguration channelConfig
    )
    {
        ChannelViewModel channel = await module.ExecuteCommandAsync(
            new CreateCommunityChannelCommand(
                channelConfig.Name,
                channelConfig.Type,
                communityId,
                channelConfig.ParentId
            )
        );

        return Results.Ok(channel);
    }

    private static async Task<IResult> GetCommunityChannelsAsync(
        [FromServices] ICommunityModule module,
        [FromRoute] ulong communityId
    )
    {
        IEnumerable<ChannelViewModel> channels = await module.ExecuteQueryAsync(
            new GetCommunityChannelsQuery(communityId)
        );

        return Results.Ok(channels);
    }
}