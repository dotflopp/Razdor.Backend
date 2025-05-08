using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public abstract class ChildChannel: CommunityChannel
{
    public ChildChannel(
        ulong id, 
        uint position, 
        string name, 
        Community community, 
        IReadOnlyDictionary<ulong, PermissionOverwrite> rolesPermission, 
        IReadOnlyDictionary<ulong, PermissionOverwrite> usersPermission, 
        CommunityChannel? parent, 
        bool isSyncing
    ) : base(id, position, name, community) {
        Parent = parent;
        IsSyncing = isSyncing;
    }

    /// <summary>
    /// CategoryChannel либо родительского MessageChannel для ForkChannel
    /// </summary>
    CommunityChannel? Parent { get; }
    
    /// <summary>
    /// Указывает на то что права синхронизируются с родительским каналом
    /// </summary>
    bool IsSyncing { get; }
}