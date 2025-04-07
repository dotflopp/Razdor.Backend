using Mediator;
using Razdor.Identity.Module.Commands.ViewModels;

namespace Razdor.Identity.Module.Commands;

public class SignupCommandHandler : ICommandHandler<SignupCommand, AuthenticationResult>
{
    public ValueTask<AuthenticationResult> Handle(SignupCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}