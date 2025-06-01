using Microsoft.AspNetCore.Mvc;
using Razdor.Api.Routes.Communities.Channels.ViewModels;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Channels.Commands;
using Razdor.Communities.Module.Services.Channels.Queries;
using Razdor.Communities.Module.Services.Channels.ViewModels;

namespace Razdor.Api.Routes.Communities.Channels;

public static class ChannelsRouter
{
    public static IEndpointRouteBuilder MapCommunityChannels(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("{communityId:ulong}/channels");

        api.MapGet("/", GetCommunityChannels)
            .Produces<IEnumerable<ChannelViewModel>>()
            .WithSummary("Получить каналы сообщества");
        api.MapPost("/", CreateCommunityChannel)
            .Produces<ChannelViewModel>()
            .WithSummary("Создать новый канал в сообществе");

        api.MapPost("{channelId:ulong}/connect", ConnectChannelAsync)
            .Produces<SessionViewModel>()
            .WithSummary("Подключиться к голосовому каналу");

        return builder;
    }
    private static async Task<IResult> ConnectChannelAsync(
        [FromServices] ICommunityModule module,
        [FromRoute] ulong communityId,
        [FromRoute] ulong channelId
    ){
        SessionViewModel session = await module.ExecuteCommandAsync(new ConnectChannelCommand(communityId, channelId));
        return Results.Ok(session);
    }

    private static async Task<IResult> CreateCommunityChannel(
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

    private static async Task<IResult> GetCommunityChannels(
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