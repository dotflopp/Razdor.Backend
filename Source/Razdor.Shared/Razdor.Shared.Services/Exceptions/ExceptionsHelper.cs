using System.Diagnostics.CodeAnalysis;

namespace Razdor.Shared.Module.Exceptions;

public static class ExceptionsHelper
{
    [DoesNotReturn]
    public static void ThrowUnauthenticatedException(Exception? innerException = null)
    {
        throw new UnauthenticatedException("User authentication is required", innerException);
    }
}