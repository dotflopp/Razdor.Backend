using Razdor.Communities.Domain.Invites;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record InviteCreatedEvent(
    Invite Invite
) : IDomainEvent;