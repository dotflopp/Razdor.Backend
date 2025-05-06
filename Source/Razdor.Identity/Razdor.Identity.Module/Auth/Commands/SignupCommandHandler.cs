using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Identity.Module.Auth.Commands.ViewModels;
using Razdor.Shared.Module.DbExceptions;

namespace Razdor.Identity.Module.Auth.Commands;

public class SignupCommandHandler(
    IPasswordHasher<UserAccount> passwordHasher,
    IUserRepository userRepository,
    SnowflakeGenerator idGenerator,
    AccessTokenSource tokenSource,
    TimeProvider timeProvider
) : ICommandHandler<SignupCommand, AccessToken>
{
    public async ValueTask<AccessToken> Handle(SignupCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);
        if (user != null) 
            throw new UserAlreadyExistsException("User with this email or identityName already exists");

        user = UserAccount.RegisterNew(
            idGenerator.Next(),
            command.IdentityName,
            nickname: null,
            avatar: null,
            email: command.Email,
            hashedPassword: null,
            time: timeProvider
        );

        user.ChangePassword(
            passwordHasher.HashPassword(user, command.Password),
            timeProvider
        );

        userRepository.Add(user);
        try
        {
            await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            var accessToken = tokenSource.CreateNew(
                new TokenClaims(
                    user.Id,
                    timeProvider.GetUtcNow()
                )
            );
            return new AccessToken(accessToken);
        }
        catch (UniqueConstraintException e)
        {
            throw new UserAlreadyExistsException("User with this email or identityName already exists", e);
        }
        
    }
}