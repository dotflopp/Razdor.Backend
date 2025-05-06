using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Channels.Permissions;

public class VoiceChannelPermissionOverwrite(
    UserPermissions grantedPermission,
    UserPermissions rejectedPermission
): ChannelPermissionOverwrite(
    grantedPermission,
    rejectedPermission,
    AvailablePermissions
), IChannelPermissionOverwrite {
    /// <summary>
    /// Допустимые для переопределения разрешения, все остальные будут отброшены
    /// </summary>
    public const UserPermissions AvailablePermissions =
        UserPermissions.ManageChannel
        | UserPermissions.ViewChannel
        | UserPermissions.Connect
        | UserPermissions.Speak
        | UserPermissions.MuteMembers
        | UserPermissions.DeafenMembers
        | UserPermissions.MoveMembers;
}