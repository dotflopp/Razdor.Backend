using Razdor.Identity.Module.Contracts;

namespace Razdor.Identity.Module.Auth.Commands;

public record LoginCommand(
    string Email,
    string Password
) : IIdentityCommand<AuthenticationResult>;