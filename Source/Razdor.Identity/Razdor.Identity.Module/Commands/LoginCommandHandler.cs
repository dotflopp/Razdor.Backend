using Mediator;
using Microsoft.AspNetCore.Identity;
using Razdor.Identity.Domain;
using Razdor.Identity.Domain.Repositories;
using Razdor.Identity.Module.Commands.ViewModels;
using Razdor.Shared.Extensions;

namespace Razdor.Identity.Module.Commands;

public class LoginCommandHandler(
    IPasswordHasher<UserAccount> passwordHasher,   
    IUserRepository userRepository,
    AuthenticationTokenFactory tokenFactory
) : ICommandHandler<LoginCommand, AuthenticationResult>
{
    public async ValueTask<AuthenticationResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        UserAccount? user = await userRepository.FindByEmailAsync(command.Email);
        
        PasswordVerificationResult verification = PasswordVerificationResult.Failed;

        if (user is { HashedPassword: not null })
        {
            verification = passwordHasher.VerifyHashedPassword(
                user, 
                user.HashedPassword, 
                command.Password
            );
        }

        if (user == null || verification == PasswordVerificationResult.Failed)
        {
            return AuthenticationError.InvalidPasswordOrEmailError;
        }

        if (verification == PasswordVerificationResult.SuccessRehashNeeded)
        {
            string newHash = passwordHasher.HashPassword(user, command.Password);
            user.ChangePassword(newHash);
            
            await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        
        return new AccessTokenViewModel(
            tokenFactory.CreateNew(user)
        );
    }
}