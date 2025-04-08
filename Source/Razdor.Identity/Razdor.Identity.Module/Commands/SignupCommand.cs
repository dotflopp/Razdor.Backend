using Mediator;
using Razdor.Identity.Module.Commands.ViewModels;

namespace Razdor.Identity.Module.Commands;

public record SignupCommand(
    string IdentityName,
    string Email,
    string Password
) : ICommand<AuthenticationResult>;