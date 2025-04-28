using Razdor.Identity.Module.Contracts;

namespace Razdor.Identity.Module.Commands;

public record SignupCommand(
    string IdentityName,
    string Email,
    string Password
) : IIdentityCommand<AuthenticationResult>;