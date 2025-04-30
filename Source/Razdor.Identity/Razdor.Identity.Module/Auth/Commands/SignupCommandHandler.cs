using Mediator;
using Microsoft.AspNetCore.Identity;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Identity.Module.Auth.Commands.ViewModels;

namespace Razdor.Identity.Module.Auth.Commands;

public class SignupCommandHandler(
    IPasswordHasher<UserAccount> passwordHasher,   
    IUserRepository userRepository,
    SnowflakeGenerator idGenerator,
    AccessTokenSource tokenSource,
    TimeProvider timeProvider
) : ICommandHandler<SignupCommand, AuthenticationResult>
{
    public async ValueTask<AuthenticationResult> Handle(SignupCommand command, CancellationToken cancellationToken)
    {
        UserAccount? user = await userRepository.FindByEmailAsync(command.Email);
        if (user != null)
        {
            return AuthenticationError.UserAlreadyExistsError;
        }

        user = UserAccount.RegisterNew(
            id: idGenerator.Next(),
            identityName: command.IdentityName,
            nickname: null,
            avatar: null,
            email: command.Email,
            hashedPassword: null,
            time: timeProvider
        );
        
        user.ChangePassword(
            passwordHasher.HashPassword(user, command.Password),
            time: timeProvider
        );
        
        userRepository.Add(user);
        await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        string accessToken = tokenSource.CreateNew(
            new AccessTokenClaims(
                user.Id,
                timeProvider.GetUtcNow()
            )
        );
        
        return new AccessToken(accessToken);
    }
}