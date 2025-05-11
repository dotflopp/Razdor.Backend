using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain;

public class Community : BaseSnowflakeEntity, INamed, ISnowflakeEntity, IEntity<ulong>
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 400;
    
    private readonly List<Role> _roles;
    
    public Community(
        ulong id, 
        ulong ownerId, 
        string name, 
        string? avatar, 
        string? description, 
        CommunityNotificationPolicy defaultNotificationPolicy, 
        List<Role> roles
    ) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
        Avatar = avatar;
        Description = description;
        DefaultNotificationPolicy = defaultNotificationPolicy;
        _roles = roles;
    }
    public ulong OwnerId { get; }
    public string Name { get; }
    public string? Avatar { get; }
    public string? Description { get; }

    public CommunityNotificationPolicy DefaultNotificationPolicy { get; }
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
}