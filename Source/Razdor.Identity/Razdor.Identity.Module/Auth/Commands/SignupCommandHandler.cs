using Mediator;
using Microsoft.AspNetCore.Identity;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Domain.Users.Rules;
using Razdor.Identity.Module.Auth.AccessTokens;
using Razdor.Identity.Module.Auth.Commands.ViewModels;
using Razdor.Shared.Domain.Rules;
using Razdor.Shared.Module;

namespace Razdor.Identity.Module.Auth.Commands;

public sealed class SignupCommandHandler(
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
        var user = UserAccount.RegisterNew(
            idGenerator.Next(),
            command.IdentityName,
            nickname:null,
            avatar:null,
            email:command.Email,
            hashedPassword:null,
            counter:counter,
            time:timeProvider
        );

        await RuleValidationHelper.ThrowIfBrokenAsync(
            new IdentityNameAndEmailMustBeUnique(counter, user.IdentityName, user.Email)
        );

        user.ChangePasswordHash(
            passwordHasher.HashPassword(user, command.Password),
            timeProvider
        );

        userRepository.Add(user);
        await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        string accessToken = tokenSource.CreateNew(
            new TokenClaims(
                user.Id,
                timeProvider.GetUtcNow()
            )
        );

        return new AccessToken(accessToken);
    }
}