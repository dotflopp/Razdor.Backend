using Mediator;

using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users.Events;

public record UserAccountCreated(
    UserAccount Account
) : IDomainEvent;