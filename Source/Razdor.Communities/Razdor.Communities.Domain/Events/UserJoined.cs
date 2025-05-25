using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Event;

public record UserJoined(
    ulong CommunityId,
    ulong UserId
) : IDomainEvent;