using System.Diagnostics.CodeAnalysis;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Messages.Module.Exceptions;

public class AttachmentNotFoundException(
    string? message = null, 
    Exception? innerException = null
) : RazdorException(ErrorCode.AttachmentNotFound , message, innerException)
{
    [DoesNotReturn]
    public static void Throw(ulong channelId, ulong messageId, ulong attachmentId)
    {
        throw new AttachmentNotFoundException($"Attachment not found {channelId}/{messageId}/{attachmentId}");
    }
}