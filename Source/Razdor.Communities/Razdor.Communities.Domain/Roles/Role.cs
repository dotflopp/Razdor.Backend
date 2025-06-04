using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Roles;

public class Role : BaseSnowflakeEntity
{
    internal Role(
        ulong id,
        string name,
        ulong communityId,
        UserPermissions permissions,
        bool isMentionable,
        uint priority,
        uint color
    ) : base(id) 
    {
        Name = name;
        CommunityId = communityId;
        Permissions = permissions;
        IsMentionable = isMentionable;
        Priority = priority;
        Color = color;
    }
    public string Name { get; private set; }
    public ulong CommunityId { get; private set; }
    public UserPermissions Permissions { get; private set; }
    public bool IsMentionable { get; private set; }
    public uint Priority { get; private set; }
    public uint Color { get; private set; }

    public static Role CreateNew(
        ulong id,
        string name,
        ulong communityId,
        UserPermissions permissions,
        bool isMentionable,
        uint priority,
        uint color
    ){
        ArgumentNullException.ThrowIfNull(name);
        ArgumentOutOfRangeException.ThrowIfEqual(id, 0ul);
        ArgumentOutOfRangeException.ThrowIfEqual(communityId, 0ul);
        ArgumentOutOfRangeException.ThrowIfEqual(priority, 0ul);

        return new Role(
            id, name, communityId, permissions, isMentionable, priority, color
        );
    }
}