using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels.Abstractions;

public abstract class CommunityChannel : BaseSnowflakeEntity, ICommunityChannel, ISnowflakeEntity, IEntity<ulong>
{
    protected CommunityChannel(
        ulong id,
        string name,
        ulong communityId,
        uint position,
        ChannelType type
    ) : base(id)
    {
        Name = name;
        CommunityId = communityId;
        Position = position;
        Type = type;
    }

    public string Name { get; }
    public ulong CommunityId { get; }
    public ChannelType Type { get; }
    public uint Position { get; }
    
    public abstract IReadOnlyList<Overwrite> Overwrites { get; }

    public virtual UserPermissions CalculateUserPermissions(ICommunityUser user)
    {
        return ChannelHelper.CalculateUserPermissions(this, user);
    }

}