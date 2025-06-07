using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record MemberRolesChangedEvent(
    ulong CommunityId, ulong UserId, IReadOnlyCollection<ulong> Roles
) : IDomainEvent;