using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Permissions;

public interface IOverwritesPermissions
{
    IReadOnlyDictionary<ulong, PermissionOverwrite> RolesPermission { get; }
    IReadOnlyDictionary<ulong, PermissionOverwrite> UsersPermission { get; }
    
    public UserPermissions CalculateUserPermissions(ICommunityUser user);
}