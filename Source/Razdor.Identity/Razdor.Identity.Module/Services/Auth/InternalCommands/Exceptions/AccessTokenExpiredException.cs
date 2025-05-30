using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Auth.InternalCommands.Exceptions;

public class AccessTokenExpiredException(
    string message,
    Exception? innerException = null
) : RazdorException(ErrorCode.AccessTokenExpired, message, innerException)
{
    [DoesNotReturn]
    public static void Throw() 
        => throw new AccessTokenExpiredException("Request a new token, the old one was expired");
}