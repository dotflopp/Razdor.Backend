using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Auth.Commands.ViewModels;

public abstract class AuthenticationException(
    ErrorCodes code,
    string message,
    Exception? innerException = null
) : RazdorException(code, message, innerException);

public class InvalidPasswordOrEmailException(
    string message,
    Exception? innerException = null
) : AuthenticationException(ErrorCodes.InvalidPasswordOrEmail, message, innerException);

public class UserAlreadyExistsException(
    string message,
    Exception? innerException = null
) : AuthenticationException(ErrorCodes.UserAlreadyExists, message, innerException);