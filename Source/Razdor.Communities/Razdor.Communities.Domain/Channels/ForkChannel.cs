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
        ICommunityChannel parent,
        List<Overwrite> overwrites
    ) : base(id, name, communityId, position)
    {
        Parent = parent;
    }

    public override IReadOnlyList<Overwrite> Overwrites => Parent.Overwrites;
    public ICommunityChannel Parent { get; }
    public bool IsSyncing => true;

    public override UserPermissions CalculateUserPermissions(ICommunityUser user)
    { 
        UserPermissions permissions = base.CalculateUserPermissions(user);
        
        if (permissions.HasFlag(UserPermissions.SendMessageInFork))
            permissions |= UserPermissions.SendMessage;
        
        return permissions;
    }
}
