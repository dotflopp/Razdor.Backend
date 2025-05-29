using Razdor.Communities.Domain.Events;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Invites;

public class Invite : BaseAggregateRoot, IEntity<string>
{
    internal Invite(
        string id,
        ulong communityId,
        ulong creatorId,
        DateTimeOffset? expiresAt,
        DateTimeOffset createdAt,
        uint usesCount
    )
    {
        Id = id;
        CommunityId = communityId;
        CreatorId = creatorId;
        ExpiresAt = expiresAt;
        CreatedAt = createdAt;
        UsesCount = usesCount;
    }

    public ulong CommunityId { get; init; }
    public ulong CreatorId { get; init; }
    public DateTimeOffset? ExpiresAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public uint UsesCount { get; init; }

    public string Id { get; }
    public bool IsTransient => string.IsNullOrEmpty(Id);

    public static Invite Create(string id, ulong creatorId, ulong communityId, TimeSpan? lifeTime, TimeProvider? timeProvider = null)
    {
        DateTimeOffset now = timeProvider?.GetUtcNow() ?? DateTimeOffset.Now;
        DateTimeOffset? expireTime = null;

        if (lifeTime.HasValue)
            expireTime = now + lifeTime.Value;

        var invite = new Invite(id, communityId, creatorId, expireTime, now, 0);

        invite.AddDomainEvent(new InviteCreated(communityId, invite.Id));

        return invite;
    }
}