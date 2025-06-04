using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

/// <summary>
/// Коды каналов, битовые флаги, можно использовать для проверки, но не для присваивания каналу
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ChannelType>))]
public enum ChannelType
{
    CategoryChannel = 0x1,
    TextChannel = 0x2,
    VoiceChannel = 0x4,
    ForkChannel = 0x8
}

public abstract class CommunityChannel(
    ulong id,
    string name,
    ulong communityId,
    ulong parentId,
    uint position,
    ChannelType type
) : BaseSnowflakeEntity(id), IOverwritesPermission
{
    public string Name { get; set; } = name;
    public ulong ParentId { get; set; } = parentId;
    public ulong CommunityId { get; private set; } = communityId;
    public ChannelType Type { get; private set; } = type;
    public uint Position { get; set; } = position;

    public abstract bool IsSyncing { get; }
    public abstract IReadOnlyList<Overwrite> Overwrites { get; }

    /// <summary>
    ///     Вернет права пользователя с учетом перезаписанных прав в канале, наследуемые права необходимо передать отдельным
    ///     параметром
    /// </summary>
    /// <param name="member"></param>
    /// <param name="inheritedPermissions">Наследуемые параметры канала</param>
    /// <returns></returns>
    public virtual UserPermissions GetPermissionsWithOverwrites(CommunityMember member, UserPermissions inheritedPermissions)
    {
        UserPermissions result = inheritedPermissions;

        var overwrites = GetIntersectionOverwrites(member).ToList();

        foreach (Overwrite overwrite in overwrites)
            result = overwrite.Permissions.ApplyAllow(result);

        foreach (Overwrite overwrite in overwrites)
            result = overwrite.Permissions.ApplyDeny(result);

        return result;
    }

    private IEnumerable<Overwrite> GetIntersectionOverwrites(CommunityMember member)
    {
        if (Overwrites.Count == 0)
            yield break;

        using IEnumerator<ulong> roleIds = member.RoleIds.GetEnumerator();
        roleIds.MoveNext();

        foreach (Overwrite overwrite in Overwrites)
        {
            if (overwrite.TargetId == member.UserId && overwrite.TargetType == PermissionTargetType.User)
            {
                yield return overwrite;
                continue;
            }

            while (overwrite.TargetId > roleIds.Current)
                roleIds.MoveNext();

            if (roleIds.Current != overwrite.TargetId || overwrite.TargetType != PermissionTargetType.Role)
                continue;

            yield return overwrite;
        }
    }
}