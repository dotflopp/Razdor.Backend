using Microsoft.AspNetCore.Mvc;
using Razdor.Communities.Domain.Channels.Guild;
using Razdor.Communities.Domain.Repositories;
using Razdor.Communities.Domain.Repositories.Models;
using Razdor.Signaling.Internal;

namespace Razdor.Communities.Api.Guilds.Channels
{
    public static class GuildChannelEndpoints
    {
        public static IEndpointRouteBuilder MapGuildChannels(
            this IEndpointRouteBuilder builder,
            [StringSyntax("Route")] string prefix = "{guildId:ulong}/channels/"
        ){
            RouteGroupBuilder groupBuilder = builder.MapGroup(prefix)
                .WithTags("Каналы гильдий");
            
            groupBuilder.MapGet("/", GetGuildAllChannelsAsync)
                .WithSummary("Получить все каналы гильдии");
            
            groupBuilder.MapPost("/", CreateGuildChannelAsync)
                .WithSummary("Создать новый канал в гильдии");  
            
            groupBuilder.MapGet("/{channelId:ulong}", FindGuildChannelAsync)
                .WithSummary("Получить информацию об одном канале гильдии");
            
            groupBuilder.MapPost("/{channelId:ulong}/join", JoinGuildChannelAsync)
                .WithSummary("Создать сессию, если существует, вернется существующая");
            
            groupBuilder.MapGet("/{channelId:ulong}/users", GetUsersInVoiceAsync)
                .WithSummary("Получить список пользователей в канале");
            
            return builder;
        }

        private static async Task<IResult> GetUsersInVoiceAsync(
            [FromRoute] ulong guildId,
            [FromRoute] ulong channelId,
            [FromServices] IChannelsRepository channels,
            [FromServices] ISignalingServiceProvider signalingServices
        ){
            IGuildChannel? channel = await channels.FindGuildChannelAsync(guildId, channelId);
            
            if (channel is not IGuildVoiceChannel voiceChannel)
                return Results.NotFound();
            
            if (!voiceChannel.SignalingId.HasValue)
                return Results.NotFound();

            ISignalingInternalService? signalingService = await signalingServices.FindSignalingServiceAsync(
                voiceChannel.SignalingId.Value
            );
            
            if (signalingService is null)
                return Results.NotFound();

            IRoom? room = await signalingService.FindRoomAsync(channelId);
            
            if (room is null)
                return Results.NotFound();

            IEnumerable<UserIdentity> users = await room.GetUsersAsync();
            
            return Results.Ok(users);
        }

        internal static async Task<IResult> JoinGuildChannelAsync(
            [FromRoute] ulong guildId,
            [FromRoute] ulong channelId,
            [FromServices] IChannelsRepository channels,
            [FromServices] ISignalingServiceProvider signalingServices
        ){
            IGuildChannel? channel = await channels.FindGuildChannelAsync(guildId, channelId);
            
            if (channel is not IGuildVoiceChannel voiceChannel)
                return Results.NotFound();
            
            ISignalingInternalService? signalingService = null;
            if (voiceChannel.SignalingId.HasValue)
            {
                signalingService = await signalingServices.FindSignalingServiceAsync(
                    voiceChannel.SignalingId.Value
                );
            }

            if (signalingService is null)
            {
                signalingService = await signalingServices.GetOptimalSignalingServiceAsync(
                    guildId, 
                    channelId
                );

                await channels.TrySetNewSignalingServiceAsync(voiceChannel, signalingService.Id);
            }
            
            IRoom room = await signalingService.CreateIfNotExistRoomAsync(channelId);
            IRoomSession session = await room.CreateUserSessionIfNotExistsAsync();
            
            return Results.Ok(session);
        }

        internal static async Task<IResult> CreateGuildChannelAsync(
            [FromServices] IChannelsRepository channels,
            [FromRoute] ulong guildId,
            [FromBody] ChannelCreationModel channelModel
        ){
            IGuildChannel guildChannel = await channels.CreateGuildChannelAsync(guildId, channelModel);
            
            return Results.Created(
                $"guilds/{guildId}/channels/{guildChannel.GuildId}", 
                guildChannel
            );
        }

        internal static async Task<IResult> GetGuildAllChannelsAsync(
            ulong guildId,
            [FromServices] IChannelsRepository channels
        ){
            return Results.Ok(
                await channels.GetGuildChannelsAsync(guildId)
            );
        }
            
        internal static async Task<IResult> FindGuildChannelAsync(
            [FromRoute] ulong guildId, 
            [FromRoute] ulong channelId, 
            [FromServices] IChannelsRepository channels
        ){
            return Results.Ok(
                await channels.FindGuildChannelAsync(guildId, channelId)
            );
        }
    }
}
