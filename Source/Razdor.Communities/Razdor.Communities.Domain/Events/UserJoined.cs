using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record UserJoined(
    ulong CommunityId,
    ulong UserId
) : IDomainEvent;