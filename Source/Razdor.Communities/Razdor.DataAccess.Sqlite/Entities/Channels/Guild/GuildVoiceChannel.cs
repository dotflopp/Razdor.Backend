using Razdor.Communities.Domain.Channels.Guild;

namespace Razdor.Communities.DataAccess.EF.Entities.Channels.Guild;

public class GuildVoiceChannel : GuildChannel, IGuildVoiceChannel
{
    public ulong? SignalingId { get; set; } = null;
    public required uint Bitrate { get; set; }
    public required uint UserLimit { get; set; }
}