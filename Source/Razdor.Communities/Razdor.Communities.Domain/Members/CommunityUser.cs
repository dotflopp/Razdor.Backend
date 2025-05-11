using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

public class CommunityUser : BaseSnowflakeEntity, ICommunityUser, ISnowflakeEntity, IEntity<ulong>
{
    private readonly List<Role> _roles;
    
    public CommunityUser(
        ulong id,
        ulong communityId,
        bool isOwner,
        VoiceState voiceState,
        CommunityNotificationPolicy? notificationPolicy,
        UserCommunityProfile? communityProfile,
        List<Role> roles,
        DateTimeOffset joiningDate
    ) : base(id)
    {
        _roles = roles;
        
        IsOwner = isOwner;
        VoiceState = voiceState;
        NotificationPolicy = notificationPolicy;
        CommunityProfile = communityProfile;
        JoiningDate = joiningDate;
        CommunityId = communityId;
    }

    public bool IsOwner { get; }
    public VoiceState VoiceState { get; }
    public ulong CommunityId { get; }
    public CommunityNotificationPolicy? NotificationPolicy { get; }
    public UserCommunityProfile? CommunityProfile { get; }
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    public DateTimeOffset JoiningDate { get; }
    
    public UserPermissions GetCommunityPermissions()
    {
        UserPermissions userPermissions = UserPermissions.None;

        if (IsOwner)
            userPermissions |= UserPermissions.Administrator;
        
        foreach (var role in Roles)
        {
            userPermissions |= role.Permissions;
        }
        
        return userPermissions;
    }

    public ulong GetHighestPriority()
    {
        if (IsOwner)
            return 0;
        
        return Roles.Select(x => x.Priority).Min();
    }
}
