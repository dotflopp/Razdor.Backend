using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Media.Exceptions;

public class InvalidMediaTypeException(
    string? message = null,
    Exception? innerException = null
) : RazdorException(ErrorCode.InvalidMediaType, message, innerException)
{
    [DoesNotReturn]
    public static void Throw(string mediaType)
    {
        throw new InvalidMediaTypeException($"Invalid media type {mediaType}");
    }
}