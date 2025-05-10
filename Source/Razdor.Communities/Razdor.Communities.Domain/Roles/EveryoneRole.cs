using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Roles;

public class EveryoneRole : IRole
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

public EveryoneRole(ulong communityId, UserPermissions permissions, bool isMentioned, uint priority, string name)
    {
        CommunityId = communityId;
        Permissions = permissions;
        IsMentioned = isMentioned;
        Priority = priority;
        Name = name;
    }

    public ulong Id => CommunityId;
    public string Name { get; }
    public ulong CommunityId { get; }
    public UserPermissions Permissions { get; }
    public bool IsMentioned { get; }
    public uint Priority { get; }
}