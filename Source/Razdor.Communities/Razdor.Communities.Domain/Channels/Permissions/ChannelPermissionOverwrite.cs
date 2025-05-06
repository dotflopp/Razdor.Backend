using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Channels.Permissions;

public abstract class ChannelPermissionOverwrite : IChannelPermissionOverwrite
{
    private readonly UserPermissions _grantedPermission;
    private readonly UserPermissions _rejectedPermission;
    
    public ChannelPermissionOverwrite(
        UserPermissions grantedPermission,
        UserPermissions rejectedPermission,
        UserPermissions availablePermissions
    ){
        _grantedPermission = grantedPermission & availablePermissions;
        _rejectedPermission = rejectedPermission & availablePermissions;
    }

    public virtual UserPermissions RejectPermissions(UserPermissions permissions)
    {
        return ~_rejectedPermission & permissions;
    }

    public virtual UserPermissions GrantPermissions(UserPermissions permissions)
    {
        return _grantedPermission | permissions;
    }
}