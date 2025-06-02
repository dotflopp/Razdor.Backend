using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Communities.Module.Exceptions;

public class InvalidChannelOperationException(
    string? message = null, Exception? innerException = null
) : RazdorException(ErrorCode.InvalidOperationException, message, innerException)
{
    public static void ThrowInvalidType()
    {
        throw new InvalidChannelOperationException("Invalid channel type");
    }
}