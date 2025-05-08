using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class MessageCommunityChannel : 
    ChildChannel,
    IOverwritesPermissionsOwner
{
    public MessageCommunityChannel(ulong id, uint position, string name, Community community, IReadOnlyDictionary<ulong, PermissionOverwrite> rolesPermission, IReadOnlyDictionary<ulong, PermissionOverwrite> usersPermission, CommunityChannel? parent, bool isSyncing) : base(id, position, name, community, rolesPermission, usersPermission, parent, isSyncing)
    {
        Name = name;
        Id = id;
        Community = community;
        RolesPermission = rolesPermission;
        UsersPermission = usersPermission;
        Position = position;
        Parent = parent;
        IsSyncing = isSyncing;
    }

    public string Name { get; }
    public void Rename(string newName)
    {
        throw new NotImplementedException();
    }

    public ulong Id { get; }
    public Community Community { get; }
    public override IReadOnlyDictionary<ulong, PermissionOverwrite> RolesPermission { get; }
    public override IReadOnlyDictionary<ulong, PermissionOverwrite> UsersPermission { get; }
    public uint Position { get; }
        
    public void SetRolePermission(IRole role, PermissionOverwrite permission)
    {
        throw new NotImplementedException();
    }

    public void SetUserPermission(IUser user, PermissionOverwrite permission)
    {
        throw new NotImplementedException();
    }

    public void RemoveUserPermission(IUser user)
    {
        throw new NotImplementedException();
    }

    public void RemoveRolePermission(IRole role)
    {
        throw new NotImplementedException();
    }

    public CommunityChannel? Parent { get; }
    public bool IsSyncing { get; }
}