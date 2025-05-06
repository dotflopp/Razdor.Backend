using Razdor.Communities.Domain.Channels.Permissions;
using Razdor.Communities.Domain.Communities;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class VoiceCommunityChannel: CommunityChannel, IOverwritesPermissionsOwner
{
    public VoiceCommunityChannel(ulong id, uint position, string name, Community community, bool isSyncing, CommunityChannel? parent, IReadOnlyDictionary<ulong, PermissionOverwrite> rolesPermission, IReadOnlyDictionary<ulong, PermissionOverwrite> usersPermission) : base(id, position, name, community)
    {
        Id = id;
        Name = name;
        Community = community;
        IsSyncing = isSyncing;
        Position = position;
        Parent = parent;
        RolesPermission = rolesPermission;
        UsersPermission = usersPermission;
    }

    public ulong Id { get; }
    public string Name { get; }
    public Community Community { get; }
    public bool IsSyncing { get; }
    public uint Position { get; }
    public CommunityChannel? Parent { get; }

    public override IReadOnlyDictionary<ulong, PermissionOverwrite> RolesPermission { get; }
    public override IReadOnlyDictionary<ulong, PermissionOverwrite> UsersPermission { get; }
    
    public UserPermissions CalculateUserPermissions(ICommunityUser user)
    {
        throw new NotImplementedException();
    }

    public void Rename(string newName)
    {
        throw new NotImplementedException();
    }
    
    public void SetRolePermission(IRole role, PermissionOverwrite permission)
    {
        throw new NotImplementedException();
    }

    public void SetUserPermission(IUser user, PermissionOverwrite permission)
    {
        // UsersPermission[user.Id] = permission;
    }

    public void RemoveUserPermission(IUser user)
    {
        throw new NotImplementedException();
    }

    public void RemoveRolePermission(IRole role)
    {
        throw new NotImplementedException();
    }
}