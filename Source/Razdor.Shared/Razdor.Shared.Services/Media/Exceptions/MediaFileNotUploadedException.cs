using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public class MediaFileNotUploadedException(
    string? message = null, 
    Exception? innerException = null
) : RazdorException(ErrorCode.MediaNotUploaded, message, innerException)
{

    [DoesNotReturn]
    public static void Throw()
    {
        throw new MediaFileNotUploadedException("Couldn't upload media file");
    }
}