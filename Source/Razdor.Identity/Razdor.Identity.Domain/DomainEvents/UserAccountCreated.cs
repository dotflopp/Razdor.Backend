using Mediator;
using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.DomainEvents;

public record UserAccountCreated(
    UserAccount Account
) : IDomainEvent;