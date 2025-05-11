using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

public class CommunityUser : BaseSnowflakeEntity, ICommunityUser, ISnowflakeEntity, IEntity<ulong>
{
    public CommunityUser(
        ulong id, 
        bool isOwner,
        VoiceState voiceState,
        CommunityNotificationPolicy? notificationPolicy, 
        UserCommunityProfile? communityProfile, 
        IReadOnlyCollection<IRole> roles, 
        UserPermissions communityPermissions, 
        ulong highestPriority, 
        DateTimeOffset joiningDate
    ) : base(id)
    {
        IsOwner = isOwner;
        VoiceState = voiceState;
        NotificationPolicy = notificationPolicy;
        CommunityProfile = communityProfile;
        Roles = roles;
        CommunityPermissions = communityPermissions;
        HighestPriority = highestPriority;
        JoiningDate = joiningDate;
    }

    public bool IsOwner { get; }
    public VoiceState VoiceState { get; }
    public CommunityNotificationPolicy? NotificationPolicy { get; }
    public UserCommunityProfile? CommunityProfile { get; }
    public IReadOnlyCollection<IRole> Roles { get; }
    public UserPermissions CommunityPermissions { get; }
    public  ulong HighestPriority { get; }
    public DateTimeOffset JoiningDate { get; }
}