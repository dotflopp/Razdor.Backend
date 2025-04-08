using Mediator;
using Microsoft.AspNetCore.Identity;
using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Repositories;
using Razdor.Identity.Module.Commands.ViewModels;
using Razdor.Shared.Features;

namespace Razdor.Identity.Module.Commands;

public class SignupCommandHandler(
    IPasswordHasher<UserAccount> passwordHasher,   
    IUserRepository userRepository,
    SnowflakeGenerator idGenerator,
    AuthenticationTokenFactory tokenFactory   
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
            email: command.Email,
            passwordHash: null
        );
        
        user.ChangePassword(
            passwordHasher.HashPassword(user, command.Password)
        );
        
        userRepository.Add(user);
        await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
        return new AccessTokenViewModel(
            tokenFactory.CreateNew(user)
        );
    }
}