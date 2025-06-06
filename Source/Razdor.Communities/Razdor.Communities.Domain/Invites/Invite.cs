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

    public ulong CommunityId { get; private set;  }
    public ulong CreatorId { get; private set;  }
    public DateTimeOffset? ExpiresAt { get; private set;  }
    public DateTimeOffset CreatedAt { get; private set; }
    public uint UsesCount { get; set; }

    public string Id { get; private set; }

    public static Invite Create(string id, ulong creatorId, ulong communityId, TimeSpan? lifeTime, TimeProvider? timeProvider = null)
    {
        DateTimeOffset now = timeProvider?.GetUtcNow() ?? DateTimeOffset.Now;
        DateTimeOffset? expireTime = null;

        if (lifeTime.HasValue)
            expireTime = now + lifeTime.Value;

        var invite = new Invite(id, communityId, creatorId, expireTime, now, 0);

        invite.AddDomainEvent(new InviteCreatedEvent(invite));

        return invite;
    }
}