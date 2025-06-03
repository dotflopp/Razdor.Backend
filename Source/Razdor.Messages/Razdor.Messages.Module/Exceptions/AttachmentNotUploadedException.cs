using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Messages.Module.Exceptions;

public class AttachmentNotUploadedException(
    string? message = null, 
    Exception? innerException = null
) : RazdorException(ErrorCode.AttachmentNotUploaded, message, innerException)
{

    public static void Throw()
    {
        throw new AttachmentNotUploadedException("Attachment not uploaded");
    }
}