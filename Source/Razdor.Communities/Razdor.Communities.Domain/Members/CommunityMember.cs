using System.Collections.ObjectModel;
using Razdor.Communities.Domain.Events;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

public class CommunityMember : BaseAggregateRoot
{
    private List<ulong>? _roleIds;

    /// <summary>
    ///     EF constructor
    /// </summary>
    private CommunityMember() : this(0, 0, null!, null, null, DateTimeOffset.MinValue)
    {
        Console.WriteLine("Ef core constructor");
    }

    internal CommunityMember(
        ulong userId,
        ulong communityId,
        VoiceState voiceState,
        CommunityNotificationPolicy? notificationPolicy,
        List<ulong>? roleIds,
        DateTimeOffset joiningDate
    )
    {
        _roleIds = roleIds;
        UserId = userId;
        CommunityId = communityId;
        VoiceState = voiceState;
        NotificationPolicy = notificationPolicy;
        JoiningDate = joiningDate;
    }

    public ulong UserId { get; private set; }
    public ulong CommunityId { get; private set; }

    public VoiceState VoiceState { get; private set; }

    /// <summary>
    ///     Переопределенная политика уведомлений для конкретного пользователя
    /// </summary>
    public CommunityNotificationPolicy? NotificationPolicy { get; private set; }

    /// <summary>
    ///     Отсортированная в порядке возрастания коллекция ролей
    /// </summary>
    public IReadOnlyCollection<ulong> RoleIds
    {
        get => _roleIds?.AsReadOnly() ?? ReadOnlyCollection<ulong>.Empty;
        set => throw new NotImplementedException();
    }

    /// <summary>
    /// Переопределенный профиль пользователя
    /// </summary>
    /// public MemberProfile? Profile { get; set; }


    /// <summary>
    ///     Дата последнего присоединения к сообществу
    /// </summary>
    public DateTimeOffset JoiningDate { get; private set; }

    public static CommunityMember CreateNew(ulong userId, ulong communityId, TimeProvider? time = null)
    {
        CommunityMember member = new(
            userId,
            communityId,
            VoiceState.Default,
            null,
            [],
            time?.GetUtcNow() ?? DateTimeOffset.UtcNow
        );

        member.AddDomainEvent(new CommunityMemberAddedEvent(member));
        return member;
    }
    
    public void AddRole(Role role)
    {
        _roleIds ??= new();
        _roleIds.Add(role.Id);
        
        AddDomainEvent(new MemberRolesChangedEvent(CommunityId, UserId, _roleIds));
    }
}