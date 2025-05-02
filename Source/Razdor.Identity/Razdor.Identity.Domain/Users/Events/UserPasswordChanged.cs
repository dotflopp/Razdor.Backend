using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users.Events;

public record UserPasswordChanged(
    UserAccount User,
    string? OldHashedPassword
) : IDomainEvent;