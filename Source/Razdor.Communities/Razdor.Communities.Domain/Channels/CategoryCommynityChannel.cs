using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class CategoryCommynityChannel: CommunityChannel, IOverwritesPermissionsOwner
{   
    public CategoryCommynityChannel(ulong id, uint position, string name, Community community, Dictionary<ulong, PermissionOverwrite> rolesPermission, Dictionary<ulong, PermissionOverwrite> usersPermission) : base(id, position, name, community)
    {
    }
    
    public override IReadOnlyDictionary<ulong, PermissionOverwrite> RolesPermission { get; }
    public override IReadOnlyDictionary<ulong, PermissionOverwrite> UsersPermission { get; }
    

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

}