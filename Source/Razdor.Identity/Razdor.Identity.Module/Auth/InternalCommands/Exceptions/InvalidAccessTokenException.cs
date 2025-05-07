using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Auth.InternalCommands.Exceptions;

public class InvalidAccessTokenException(
    string message,
    Exception? innerException = null
) : RazdorException(ErrorCode.InvalidAccessToken, message, innerException);

public class AccessTokenExpiredException(
    string message,
    Exception? innerException = null
) : RazdorException(ErrorCode.AccessTokenExpired, message, innerException);