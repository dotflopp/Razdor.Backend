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
            ValidationExceptionHelper.ThrowInvalidAccessTokenException();

        var tokenClaims = ReadToken(command.AccessToken);
        var user = await users.FindByIdAsync(tokenClaims.UserId);

        if (user == null)
            ValidationExceptionHelper.ThrowInvalidAccessTokenException();

        if (user.CredentialsChangeDate > tokenClaims.CreationTime)
            ValidationExceptionHelper.ThrowTokenExpiredException();

        return new UserClaims(
            tokenClaims.UserId
        );
    }

    private TokenClaims ReadToken(string token)
    {
        try
        {
            return tokenSource.Read(token);
        }
        catch (Exception ex)
        {
            ValidationExceptionHelper.ThrowInvalidAccessTokenException(ex);
            return null!;
        }
    }
}