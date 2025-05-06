using System.ComponentModel;
using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Channels.Permissions;

public record PermissionOverwrite(
    UserPermissions GrantedPermission,
    UserPermissions RejectedPermission
){
    public static readonly PermissionOverwrite Default = new(
        UserPermissions.None,
        UserPermissions.None
    );

    public UserPermissions RejectPermissions(UserPermissions permissions)
    {
        return ~RejectedPermission & permissions;
    }

    public UserPermissions GrantPermissions(UserPermissions permissions)
    {
        return GrantedPermission | permissions;
    }
}