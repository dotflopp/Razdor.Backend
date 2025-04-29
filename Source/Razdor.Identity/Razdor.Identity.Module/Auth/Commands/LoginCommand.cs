using Mediator;
using Razdor.Identity.Module.Commands.ViewModels;
using Razdor.Identity.Module.Contracts;

namespace Razdor.Identity.Module.Commands;

public record LoginCommand(
    string Email,
    string Password
): IIdentityCommand<AuthenticationResult>;