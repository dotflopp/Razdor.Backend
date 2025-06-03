using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users.Events;

public record UserAvatarChanged(
    ulong UserId
) : IDomainEvent;