using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Auth.Commands.ViewModels;

public abstract class AuthenticationException(
    ErrorCode errorCode,
    string message,
    Exception? innerException = null
) : RazdorException(errorCode, message, innerException);

public class InvalidPasswordOrEmailException(
    string message,
    Exception? innerException = null
) : AuthenticationException(ErrorCode.InvalidPasswordOrEmail, message, innerException);

public class UserAlreadyExistsException(
    string message,
    Exception? innerException = null
) : AuthenticationException(ErrorCode.UserAlreadyExists, message, innerException);