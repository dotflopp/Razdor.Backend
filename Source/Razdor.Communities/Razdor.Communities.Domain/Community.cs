using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain;

public class Community : INamed, ISnowflakeEntity, IEntity<ulong>
{
    public Community(ulong id, ulong ownerId, string name, string? avatar, string description, CommunityNotificationPolicy defaultNotificationPolicy, IReadOnlyCollection<IUser> bannedUsers, IReadOnlyCollection<IRole> roles)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
        Avatar = avatar;
        Description = description;
        DefaultNotificationPolicy = defaultNotificationPolicy;
        BannedUsers = bannedUsers;
        Roles = roles;
    }

    public ulong Id { get; }
    public ulong OwnerId { get; }
    public string Name { get; }
    public string? Avatar { get; }
    public string Description { get; }

    public CommunityNotificationPolicy DefaultNotificationPolicy { get; }
    public IReadOnlyCollection<IUser> BannedUsers { get; }
    public IReadOnlyCollection<IRole> Roles { get; }
    
    public void Rename(string newName)
    {
        throw new NotImplementedException();
    }

}