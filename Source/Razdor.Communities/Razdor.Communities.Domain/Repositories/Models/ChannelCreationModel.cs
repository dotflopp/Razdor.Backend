using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.Domain.Repositories.Models;

public class ChannelCreationModel
{
    public ChannelType Type { get; set; }
    public ulong ParentId { get; set; }
    public string Name { get; set; }
    public uint Position { get; set; }
    public uint Bitrate { get; set; }
    public uint UserLimit { get; set; }
}