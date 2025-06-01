using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Communities.Module.Exceptions;

public class CommunityChannelNotFoundException(
    string? message = null, 
    Exception? innerException = null
) : RazdorException(ErrorCode.ChannelNotFound, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(ulong communityId, ulong channelId)
    {
        throw new CommunityChannelNotFoundException($"The Channel({channelId}) not found in Community({communityId})");
    }
}