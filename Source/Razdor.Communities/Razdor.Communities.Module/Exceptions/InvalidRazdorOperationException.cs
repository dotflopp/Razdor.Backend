using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Communities.Module.Exceptions;

public class InvalidRazdorOperationException(
    string? message = null, Exception? innerException = null
) : RazdorException(ErrorCode.InvalidOperationException, message, innerException)
{
    public static void ThrowInvalidChannelType()
    {
        throw new InvalidRazdorOperationException("Invalid channel type");
    }
}