using Mediator;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Identity.Module.Auth.InternalCommands.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Auth.InternalCommands;

public class ExtractUserClaimsCommandHandler(
    AccessTokenSource tokenSource,
    IUserRepository users
) : ICommandHandler<ExtractUserClaimsCommand, UserClaims>
{
    public async ValueTask<UserClaims> Handle(ExtractUserClaimsCommand command, CancellationToken cancellationToken)
    {
        if (!tokenSource.Check(command.AccessToken))
            InvalidAccessTokenException.Throw();

        TokenClaims tokenClaims = tokenSource.Read(command.AccessToken);
        UserAccount? user = await users.FindByIdAsync(tokenClaims.UserId);

        if (user == null)
            InvalidAccessTokenException.Throw();

        if (user.CredentialsChangeDate > tokenClaims.CreationTime)
            AccessTokenExpiredException.Throw();

        return new UserClaims(
            tokenClaims.UserId
        );
    }
}