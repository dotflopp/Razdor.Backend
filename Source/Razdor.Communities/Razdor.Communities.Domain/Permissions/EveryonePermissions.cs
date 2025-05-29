namespace Razdor.Communities.Domain.Permissions;

/// <summary>
///     Всегда имеет такой же ID как у Сообщества
///     Идет по умолчанию как роль каждого пользователя в сообществе
/// </summary>
public class EveryonePermissions(UserPermissions permissions, uint priority)
{
    /// <summary>
    ///     Права которые выдаются при создании роли Everyone
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

    public UserPermissions Permissions { get; private set; } = permissions;
    public uint Priority { get; private set; } = priority;

    public static EveryonePermissions Default { get; } = new(InitialPermissions, 1);
}