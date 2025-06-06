using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Communities.Module.Exceptions;

public class InvalidOperationRazdorException(
    string? message = null, Exception? innerException = null
) : RazdorException(ErrorCode.InvalidOperationException, message, innerException)
{
    public static void ThrowInvalidChannelType()
    {
        throw new InvalidOperationRazdorException("Invalid channel type");
    }
}