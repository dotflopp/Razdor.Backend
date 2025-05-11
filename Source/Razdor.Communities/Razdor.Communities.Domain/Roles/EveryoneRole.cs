using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Roles;

public class EveryoneRole : Role
{
    /// <summary>
    /// Права которые выдаются при создании роли Everyone
    /// </summary>
    public const UserPermissions InitialPermissions =
         UserPermissions.ViewChannel
        | UserPermissions.SendMessage
        | UserPermissions.AttachFiles
        | UserPermissions.AttachEmbed
        | UserPermissions.UseEmoji
        | UserPermissions.CreateFork
        | UserPermissions.SendMessageInFork
        | UserPermissions.ChangeNickname
        | UserPermissions.InviteMembers
        | UserPermissions.Connect
        | UserPermissions.Speak;

    public EveryoneRole(ulong communityId, UserPermissions permissions, uint priority)
        : base(communityId, "Everyone", communityId, permissions, true, priority)
    { }
}