using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Roles;

public class Role(
    ulong id, 
    string name, 
    ulong communityId,
    UserPermissions permissions, 
    bool isMentioned, 
    uint priority
) : BaseSnowflakeEntity(id), IRole
{
    public string Name { get; } = name;
    public ulong CommunityId { get; } = communityId;
    public UserPermissions Permissions { get; } = permissions;
    public bool IsMentioned { get; } = isMentioned;
    public uint Priority { get; } = priority;
}