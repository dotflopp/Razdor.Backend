using Mediator;
using Microsoft.AspNetCore.Identity;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Identity.Module.Auth.Commands.ViewModels;

namespace Razdor.Identity.Module.Auth.Commands;

public class LoginCommandHandler(
    IPasswordHasher<UserAccount> passwordHasher,
    IUserRepository userRepository,
    AccessTokenSource tokenSource,
    TimeProvider timeProvider
) : ICommandHandler<LoginCommand, AuthenticationResult>
{
    public async ValueTask<AuthenticationResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(command.Email);

        var verification = PasswordVerificationResult.Failed;

        if (user is { HashedPassword: not null })
            verification = passwordHasher.VerifyHashedPassword(
                user,
                user.HashedPassword,
                command.Password
            );

        if (user == null || verification == PasswordVerificationResult.Failed)
            return AuthenticationError.InvalidPasswordOrEmailError;

        if (verification == PasswordVerificationResult.SuccessRehashNeeded)
        {
            var newHash = passwordHasher.HashPassword(user, command.Password);
            user.ChangePassword(newHash);

            await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        var accessToken = tokenSource.CreateNew(
            new TokenClaims(
                user.Id,
                timeProvider.GetUtcNow()
            )
        );

        return new AccessToken(accessToken);
    }
}