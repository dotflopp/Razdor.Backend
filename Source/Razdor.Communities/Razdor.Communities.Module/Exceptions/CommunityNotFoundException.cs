using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Communities.Services.Exceptions;

public sealed class CommunityNotFoundException(
    string? message = null,
    Exception? innerException = null
) : RazdorException(ErrorCode.CommunityNotFound, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(ulong communityId)
    {
        throw new CommunityNotFoundException($"Community({communityId}) not found)");
    }
}