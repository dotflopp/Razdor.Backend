using Mediator;

using Microsoft.AspNetCore.Identity;

using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Auth.Commands.ViewModels;
using Razdor.Identity.Module.Commands.ViewModels;
using Razdor.Shared.Features;

namespace Razdor.Identity.Module.Commands;

public class SignupCommandHandler(
    IPasswordHasher<UserAccount> passwordHasher,   
    IUserRepository userRepository,
    SnowflakeGenerator idGenerator,
    AccessTokenFactory tokenFactory   
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
            hashedPassword: null
        );
        
        user.ChangePassword(
            passwordHasher.HashPassword(user, command.Password)
        );
        
        userRepository.Add(user);
        await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
        return new AccessToken(
            tokenFactory.CreateNew(user)
        );
    }
}