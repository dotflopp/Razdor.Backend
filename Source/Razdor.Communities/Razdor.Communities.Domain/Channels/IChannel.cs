namespace Razdor.Communities.Domain.Channels;

public interface IChannel: IEntity
{
    ChannelType Type { get; }
}

