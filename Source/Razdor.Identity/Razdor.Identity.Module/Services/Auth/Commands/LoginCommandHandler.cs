﻿using Mediator;
using Microsoft.AspNetCore.Identity;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Services.Auth.AccessTokens;
using Razdor.Identity.Module.Services.Auth.Commands.Exceptions;
using Razdor.Identity.Module.Services.Auth.Commands.ViewModels;

namespace Razdor.Identity.Module.Services.Auth.Commands;

public sealed class LoginCommandHandler(
    IPasswordHasher<UserAccount> passwordHasher,
    IUserRepository userRepository,
    AccessTokenSource tokenSource,
    TimeProvider timeProvider
) : ICommandHandler<LoginCommand, AccessToken>
{
    public async ValueTask<AccessToken> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        UserAccount? user = await userRepository.FindByEmailAsync(command.Email);

        PasswordVerificationResult verification = PasswordVerificationResult.Failed;

        if (user is { HashedPassword: not null })
            verification = passwordHasher.VerifyHashedPassword(
                user,
                user.HashedPassword,
                command.Password
            );

        if (user == null || verification == PasswordVerificationResult.Failed)
            throw new InvalidPasswordOrEmailException("Invalid password or email");

        if (verification == PasswordVerificationResult.SuccessRehashNeeded)
        {
            string newHash = passwordHasher.HashPassword(user, command.Password);
            user.ChangePasswordHash(newHash);

            await userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        string accessToken = tokenSource.CreateNew(
            new TokenClaims(
                user.Id,
                timeProvider.GetUtcNow()
            )
        );

        return new AccessToken(accessToken);
    }
}