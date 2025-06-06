using Razdor.Communities.Domain.Members;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record CommunityMemberAddedEvent(
    CommunityMember Member
) : IDomainEvent;