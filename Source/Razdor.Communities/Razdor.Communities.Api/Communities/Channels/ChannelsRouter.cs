using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Api.Communities.Channels.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Channels.Commands;
using Razdor.Communities.Services.Services.Channels.Queries;

namespace Razdor.Communities.Api.Communities.Channels;

public static class ChannelsRouter
{
    public static IEndpointRouteBuilder MapCommunityChannels(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder api = builder.MapGroup("{communityId:ulong}/channels");

        api.MapGet("/", GetCommunityChannels);
        api.MapPost("/", CreateCommunityChannel);
        
        return builder;
    }

    private static async Task<IResult> CreateCommunityChannel(
        [FromServices] ICommunityModule module,
        [FromRoute] ulong communityId,
        [FromBody] CommunityChannelConfiguration channelConfig
    ){
       ChannelViewModel channel = await module.ExecuteCommandAsync(new CreateCommunityChannelCommand(
            channelConfig.Name,
            channelConfig.Type,
            communityId,
            channelConfig.ParentId
        ));

        return Results.Ok(channel);
    }
    
    private static async Task<IResult> GetCommunityChannels(       
        [FromServices] ICommunityModule module,
        [FromRoute] ulong communityId
    ){
        IEnumerable<ChannelViewModel> channels = await module.ExecuteQueryAsync(
            new GetCommunityChannelsQuery(communityId)
        );
        
        return Results.Ok(channels);
    }
}