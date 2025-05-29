using System.Collections.ObjectModel;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

public class CommunityMember : BaseAggregateRoot
{
    private readonly List<ulong>? _roleIds;

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
    public IReadOnlyCollection<ulong> RoleIds => _roleIds?.AsReadOnly() ?? ReadOnlyCollection<ulong>.Empty;
    // public List<ulong>? RoleIds 


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

        // member.AddDomainEvent(new UserJoined(communityId, userId));
        return member;
    }
}