using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users.Events;

public record UserCommunicationStatusChanged(
    ulong UserId,
    DisplayedCommunicationStatus OldStatus,
    DisplayedCommunicationStatus NewStatus
) : IDomainEvent;