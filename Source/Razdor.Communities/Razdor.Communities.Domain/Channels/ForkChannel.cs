using Razdor.Communities.Domain.Channels.Permissions;
using Razdor.Communities.Domain.Communities;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class ForkChannel: ChildChannel
{
    public ForkChannel(
        ulong id, 
        uint position, 
        string name, 
        Community community, 
        IReadOnlyDictionary<ulong, PermissionOverwrite> rolesPermission, 
        IReadOnlyDictionary<ulong, PermissionOverwrite> usersPermission, 
        CommunityChannel? parent, 
        bool isSyncing
    ) : base(id, position, name, community, rolesPermission, usersPermission, parent, isSyncing)
    {
        
    }

    public override IReadOnlyDictionary<ulong, PermissionOverwrite> RolesPermission { get; }
    public override IReadOnlyDictionary<ulong, PermissionOverwrite> UsersPermission { get; }
}
