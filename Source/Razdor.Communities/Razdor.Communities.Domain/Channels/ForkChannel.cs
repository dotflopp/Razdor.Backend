using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public class ForkChannel: CommunityChannel, IChildChannel, ISnowflakeEntity, IEntity<ulong>
{
    internal ForkChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position,
        ICommunityChannel parent
    ) : base(id, name, communityId, position, ChannelType.Fork)
    {
        Parent = parent;
    }

    public override IReadOnlyList<Overwrite> Overwrites => Parent.Overwrites;
    public ICommunityChannel Parent { get; }
    public bool IsSyncing => true;

    public override UserPermissions GetUserPermissions(ICommunityUser user)
    { 
        UserPermissions permissions = base.GetUserPermissions(user);
        
        if (permissions.HasFlag(UserPermissions.SendMessageInFork))
            permissions |= UserPermissions.SendMessage;
        
        return permissions;
    }
}
