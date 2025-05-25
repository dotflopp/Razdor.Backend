using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using Razdor.Communities.Domain.Event;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

public class CommunityMember : BaseSnowflakeEntity, IEntity<ulong>
{
    private readonly List<ulong>? _roleIds;
    
    /// <summary>
    /// EF constructor
    /// </summary>
    private CommunityMember(): this(0, 0, null!, null, null, null, DateTimeOffset.MinValue) { }
    
    public CommunityMember(
        ulong id,
        ulong communityId,
        VoiceState voiceState,
        CommunityNotificationPolicy? notificationPolicy,
        UserCommunityProfile? communityProfile,
        List<ulong>? roleIds,
        DateTimeOffset joiningDate
    ) : base(id)
    {
        _roleIds = roleIds;
        CommunityId = communityId;
        VoiceState = voiceState;
        NotificationPolicy = notificationPolicy;
        CommunityProfile = communityProfile;
        JoiningDate = joiningDate;
    }
    public ulong CommunityId { get; private set; }
    public VoiceState VoiceState { get; private set; }
    
    /// <summary>
    /// Переопределенная политика уведомлений для конкретного пользователя
    /// </summary>
    public CommunityNotificationPolicy? NotificationPolicy { get; private set; }
    
    /// <summary>
    /// Переопределенный для сообщества профиль, если есть
    /// </summary>
    public UserCommunityProfile? CommunityProfile { get; private set; }
    
    /// <summary>
    /// Отсортированная в порядке возрастания коллекция ролей
    /// </summary>
    public IReadOnlyCollection<ulong> RoleIds => _roleIds?.AsReadOnly() ?? ReadOnlyCollection<ulong>.Empty;
    
    /// <summary>
    /// Дата последнего присоединения к сообществу
    /// </summary>
    public DateTimeOffset JoiningDate { get; private set; }

    public static CommunityMember CreateNew(ulong userId, ulong communityId, TimeProvider? time = null)
    {
        CommunityMember member = new(
            userId,
            communityId,
            VoiceState.Default,
            null,
            null,
            [communityId],
            time?.GetUtcNow() ?? DateTimeOffset.UtcNow
        );
        
        member.AddDomainEvent(new UserJoined(communityId, userId));
        return member;
    }
}
