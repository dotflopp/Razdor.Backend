using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Permissions;

public interface IOverwritesOwner : IOverwritesPermission
{
    void SetRolePermission(ulong roleId, OverwritePermissions permission);
    void SetUserPermission(ulong userId, OverwritePermissions permission);
    void RemoveUserPermission(ulong userId);
    void RemoveRolePermission(ulong roleId);
}