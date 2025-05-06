using Razdor.Communities.Domain.Members;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Communities;

public interface ICommunity : ISnowflakeEntity, IEntity<ulong>
{
    ulong OwnerId { get; }
    
    string Name { get; }
    string? Avatar { get; }
    string Description { get; }

    CommunityNotificationPolicy DefaultNotificationPolicy { get; }
    
    IReadOnlyCollection<IUser> BannedUsers { get; }
    IReadOnlyCollection<IRole> Roles { get; }
}