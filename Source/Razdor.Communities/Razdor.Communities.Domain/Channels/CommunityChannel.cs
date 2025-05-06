using Razdor.Communities.Domain.Channels.Permissions;
using Razdor.Communities.Domain.Communities;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public abstract class CommunityChannel : 
    BaseSnowflakeEntity, 
    INamed, 
    ISnowflakeEntity, 
    ICommunityEntity<ulong>, 
    IOverwritesPermissions
{
    public CommunityChannel(
        ulong id, 
        uint position, 
        string name, 
        Community community
    ) : base(id) {
        Position = position;
        Name = name;
        Community = community;
    }
    /// <summary>
    /// Позиция канала в категории
    /// </summary>
    public uint Position { get; private set; }
    public string Name { get; private set; }
    public Community Community { get; private set; }
    public abstract IReadOnlyDictionary<ulong, PermissionOverwrite> RolesPermission { get; }
    public abstract IReadOnlyDictionary<ulong, PermissionOverwrite> UsersPermission { get; }

    public void Rename(string newName)
    {
        Name = newName;
    }

    public virtual UserPermissions CalculateUserPermissions(ICommunityUser user)
    {
        UserPermissions result = user.CommunityPermissions;
        PermissionOverwrite userPermissions = UsersPermission.GetValueOrDefault(
            user.Id, 
            PermissionOverwrite.Default
        );
        
        foreach (IRole role in user.Roles)
        {
            PermissionOverwrite rolePermission = RolesPermission.GetValueOrDefault(
                role.Id, 
                PermissionOverwrite.Default
            );

            result = rolePermission.RejectPermissions(result);
        }

        foreach (IRole role in user.Roles)
        {
            PermissionOverwrite rolePermission = RolesPermission.GetValueOrDefault(
                role.Id,
                PermissionOverwrite.Default
            );

            result = rolePermission.GrantPermissions(result);
        }
        
        result = userPermissions.RejectPermissions(result);
        
        return userPermissions.GrantPermissions(result);
    }
}
