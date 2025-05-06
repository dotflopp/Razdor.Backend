using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Channels.Permissions;


public class MessageChannelPermissionOverwrite(
    UserPermissions grantedPermission,
    UserPermissions rejectedPermission
) : ChannelPermissionOverwrite(
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
        | UserPermissions.SendMessage
        | UserPermissions.ManageMessages
        | UserPermissions.AttachFiles
        | UserPermissions.AttachEmbed
        | UserPermissions.UseEmoji
        | UserPermissions.MentionEveryone
        | UserPermissions.ManageFork
        | UserPermissions.CreateFork
        | UserPermissions.SendMessagesInFork;
}
