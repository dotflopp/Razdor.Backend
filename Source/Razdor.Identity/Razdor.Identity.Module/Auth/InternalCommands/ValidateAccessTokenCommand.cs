using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Razdor.Identity.Module.Contracts;

namespace Razdor.Identity.Module.Auth.InternalCommands
{
    public record ValidateAccessTokenCommand(
        string AccessToken
    ): IIdentityCommand<AccessTokenValidationResult>;
}
