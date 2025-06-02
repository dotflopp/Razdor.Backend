using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Communities.Module.Exceptions;

public class ChannelNotFoundException(
    string? message = null, 
    Exception? innerException = null
) : RazdorException(ErrorCode.ChannelNotFound, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(ulong channelId)
    {
        throw new ChannelNotFoundException($"The Channel({channelId}) not found");
    }
}