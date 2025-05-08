using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Permissions;

public interface IOverwritesPermissionsOwner : IOverwritesPermissions
{
    void SetRolePermission(IRole role, PermissionOverwrite permission);
    void SetUserPermission(IUser user, PermissionOverwrite permission);
    void RemoveUserPermission(IUser user);
    void RemoveRolePermission(IRole role);
}