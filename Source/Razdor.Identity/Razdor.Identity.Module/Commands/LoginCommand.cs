using Mediator;
using Razdor.Identity.Module.Commands.ViewModels;

namespace Razdor.Identity.Module.Commands;

public record LoginCommand(
    string Email,
    string Password
): ICommand<AuthenticationResult>;