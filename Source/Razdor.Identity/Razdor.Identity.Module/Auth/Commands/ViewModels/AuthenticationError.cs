using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Auth.Commands.ViewModels;

public abstract class AuthenticationError(
    ErrorCodes code,
    string message
) : BaseError(code, message)
{
    public static readonly InvalidPasswordOrEmailError InvalidPasswordOrEmailError =
        new("Invalid Password or Email");

    public static readonly UserAlreadyExistsError UserAlreadyExistsError =
        new("User already exists");
}

public class InvalidPasswordOrEmailError(
    string message
) : AuthenticationError(ErrorCodes.InvalidPasswordOrEmail, message);

public class UserAlreadyExistsError(
    string message
) : AuthenticationError(ErrorCodes.UserAlreadyExists, message);