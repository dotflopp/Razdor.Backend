using System.Diagnostics.CodeAnalysis;

namespace Razdor.Identity.Module.Auth.InternalCommands.Exceptions;

public static class ValidationExceptionHelper
{
    [DoesNotReturn]
    public static void ThrowTokenExpiredException(Exception? innerException = null)
    {
        throw new AccessTokenExpiredException("Request a new token, the old one was expired", innerException);
    }

    [DoesNotReturn]
    public static void ThrowInvalidAccessTokenException(Exception? innerException = null)
    {
        throw new InvalidAccessTokenException("Invalid access token.", innerException);
    }
}