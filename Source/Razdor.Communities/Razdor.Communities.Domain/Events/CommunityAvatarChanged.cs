using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record CommunityAvatarChanged(ulong CommunityId, MediaFileMeta? Avatar) : IDomainEvent;