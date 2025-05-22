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
) : BaseSnowflakeEntity(id)
{
    public string Name { get; private set; } = name;
    public ulong CommunityId { get; private set; } = communityId;
    public UserPermissions Permissions { get; private set; } = permissions;
    public bool IsMentioned { get; private set; } = isMentioned;
    public uint Priority { get; private set; } = priority;
}