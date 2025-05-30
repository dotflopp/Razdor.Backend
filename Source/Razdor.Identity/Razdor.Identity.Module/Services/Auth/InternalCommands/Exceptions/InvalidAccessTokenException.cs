using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Auth.InternalCommands.Exceptions;

public class InvalidAccessTokenException(
    string message,
    Exception? innerException = null
) : RazdorException(ErrorCode.InvalidAccessToken, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(Exception? innerException = null) 
        => throw new InvalidAccessTokenException("Invalid access token.", innerException);
}