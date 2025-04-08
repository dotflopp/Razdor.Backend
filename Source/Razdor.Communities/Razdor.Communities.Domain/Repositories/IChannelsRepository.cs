using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Channels.Guild;
using Razdor.Communities.Domain.Repositories.Models;

namespace Razdor.Communities.Domain.Repositories;

public interface IChannelsRepository : IRepository<IChannel>
{
    Task<IReadOnlyCollection<IGuildVoiceChannel>> GetGuildChannelsAsync(EntityId guildId);
    
    Task<IGuildChannel?> FindGuildChannelAsync(
        EntityId guildId,
        EntityId channelId
    );

    Task<IGuildChannel> CreateGuildChannelAsync(ulong guildId, ChannelCreationModel model);
    Task<bool> TrySetNewSignalingServiceAsync(IGuildChannel guildChannel, ulong signalingServiceId);
}