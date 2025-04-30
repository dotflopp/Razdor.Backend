using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediator;
using Razdor.Identity.Module.Auth.Commands.ViewModels;

namespace Razdor.Identity.Module.Auth.InternalCommands
{
    public class ValidateAccessTokenCommandHandler: ICommandHandler<ValidateAccessTokenCommand, AccessTokenValidationResult>
    {
        public ValueTask<AccessTokenValidationResult> Handle(ValidateAccessTokenCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
