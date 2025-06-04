using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record MemberRolesChanged(
    ulong CommunityId, ulong UserId, List<ulong> Roles
) : IDomainEvent;