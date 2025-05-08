using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Identity.Module.Auth.Commands.ViewModels;

namespace Razdor.Identity.Module.Auth.Commands;

public class SignupCommandHandler(
    IPasswordHasher<UserAccount> passwordHasher,
    IUserRepository userRepository,
    SnowflakeGenerator idGenerator,
    AccessTokenSource tokenSource,
    IUsersCounter counter,
    TimeProvider timeProvider
) : ICommandHandler<SignupCommand, AccessToken>
{
    public async ValueTask<AccessToken> Handle(SignupCommand command, CancellationToken cancellationToken)
    {
        UserAccount user = await UserAccount.RegisterNew(
            idGenerator.Next(),
            command.IdentityName,
            nickname: null,
            avatar: null,
            email: command.Email,
            hashedPassword: null,
            counter: counter,
            time: timeProvider
        );

        user.ChangePasswordHash(
            passwordHasher.HashPassword(user, command.Password),
            timeProvider
        );

        userRepository.Add(user);
        await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        var accessToken = tokenSource.CreateNew(
            new TokenClaims(
                user.Id,
                timeProvider.GetUtcNow()
            )
        );
        
        return new AccessToken(accessToken);
    }
}