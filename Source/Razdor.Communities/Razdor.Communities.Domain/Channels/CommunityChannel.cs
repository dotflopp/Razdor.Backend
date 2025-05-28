using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels.Abstractions;

public enum ChannelType
{
    CategoryChannel,
    TextChannel,
    VoiceChannel,
    ForkChannel
}

public abstract class CommunityChannel(
    ulong id,
    string name,
    ulong communityId,
    ulong? parentId,
    uint position,
    ChannelType type
) : BaseSnowflakeEntity(id), IOverwritesPermission
{
    public string Name { get; set; } = name;
    public ulong? ParentId { get; set; } = parentId;
    public ulong CommunityId { get; init; } = communityId;
    public ChannelType Type { get; init; } = type;
    public uint Position { get; set; } = position;

    public abstract bool IsSyncing { get; }
    public abstract IReadOnlyList<Overwrite> Overwrites { get; }

    /// <summary>
    /// Вернет права пользователя с учетом перезаписанных прав в канале, наследуемые права необходимо передать отдельным параметром
    /// </summary>
    /// <param name="member"></param>
    /// <param name="inheritedPermissions">Наследуемые параметры канала</param>
    /// <returns></returns>
    public virtual UserPermissions GetOverwrites(CommunityMember member, UserPermissions inheritedPermissions)
    {
        UserPermissions result = inheritedPermissions;
        
        List<Overwrite> overwrites = GetIntersectionOverwrites(member).ToList();
        
        foreach (var overwrite in overwrites)
            result = overwrite.Permissions.ApplyAllow(result);

        foreach (var overwrite in overwrites)
            result = overwrite.Permissions.ApplyDeny(result);
        
        return result;
    }

    private IEnumerable<Overwrite> GetIntersectionOverwrites(CommunityMember member)
    {
        if (Overwrites.Count == 0)
            yield break;
        
        using IEnumerator<ulong> roleIds = member.RoleIds.GetEnumerator();
        roleIds.MoveNext();
        
        foreach (var overwrite in Overwrites)
        {
            if (overwrite.TargetId == member.UserId && overwrite.TargetTypeType == PermissionTargetType.User)
            {
                yield return overwrite;
                continue;
            }
            
            while (overwrite.TargetId > roleIds.Current)
                roleIds.MoveNext();
            
            if (roleIds.Current != overwrite.TargetId || overwrite.TargetTypeType != PermissionTargetType.Role)
                continue;
            
            yield return overwrite;
        }
    }
}