using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record InviteCreated(
    ulong CommunityId,
    string InviteId
) : IDomainEvent;