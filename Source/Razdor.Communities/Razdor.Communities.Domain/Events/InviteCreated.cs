using Razdor.Communities.Domain.Invites;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Event;

public record InviteCreated(
    ulong CommunityId,
    string InviteId
) : IDomainEvent;