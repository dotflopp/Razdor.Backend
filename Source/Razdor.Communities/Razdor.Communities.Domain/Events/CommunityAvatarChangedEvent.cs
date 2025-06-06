using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record CommunityAvatarChangedEvent(ulong CommunityId, MediaFileMeta? Avatar) : IDomainEvent;