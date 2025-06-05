using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Media.Exceptions;

public class MediaFileNotFoundException(
    string? message = null, 
    Exception? innerException = null
) : RazdorException(ErrorCode.MediaNotFound , message, innerException)
{
    
    [DoesNotReturn]
    public static void Throw()
    {
        throw new MediaFileNotFoundException($"Media file not found.");
    }
}