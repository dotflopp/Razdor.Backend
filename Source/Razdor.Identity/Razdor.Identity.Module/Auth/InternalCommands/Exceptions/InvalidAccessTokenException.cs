using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Auth.InternalCommands.Exceptions;

public class InvalidAccessTokenException(
    string message,
    Exception? innerException = null
) : RazdorException(ErrorCodes.InvalidAccessToken, message, innerException);

public class AccessTokenExpiredException(
    string message,
    Exception? innerException = null
) : RazdorException(ErrorCodes.AccessTokenExpired, message, innerException);