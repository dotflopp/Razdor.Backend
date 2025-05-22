using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Permissions;

public interface IOverwritesOwner : IOverwritesPermission
{
    void SetRolePermission(ulong roleId, OverwritePermissions permission, List<Overwrite>? inherited);
    void SetUserPermission(ulong userId, OverwritePermissions permission, List<Overwrite>? inherited);
    void RemoveUserPermission(ulong userId, List<Overwrite>? inherited);
    void RemoveRolePermission(ulong roleId, List<Overwrite>? inherited);
}