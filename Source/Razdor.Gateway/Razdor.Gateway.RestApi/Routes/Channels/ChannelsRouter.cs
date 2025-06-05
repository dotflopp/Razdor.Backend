using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Channels.Commands;

namespace Razdor.RestApi.Routes.Channels;

public static class ChannelsRouter
{
    public static IEndpointRouteBuilder MapCommunityChannels(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("")
            .WithTags("Channels");

        api.MapPost("/channels/{channelId:ulong}/connect", ConnectChannelAsync)
            .Produces<SessionViewModel>()
            .WithSummary("Подключиться к голосовому каналу");

        return builder;
    }
    private static async Task<IResult> ConnectChannelAsync(
        [FromServices] ICommunitiesModule module,
        [FromRoute] ulong channelId
    ){
        SessionViewModel session = await module.ExecuteCommandAsync(new ConnectChannelCommand(channelId));
        return Results.Ok(session);
    }
}