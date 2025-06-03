using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.DataAccess;
using Razdor.Messages.Module.Exceptions;
using Razdor.Messages.Module.Services.Commands.ViewModels;

namespace Razdor.Messages.Module.Services.Query;

public class GetAttachmentQueryHandler(
    AttachmentsStore attachmentsStore,
    MessagesDbContext context
) : IQueryHandler<GetAttachmentQuery, AttachmentFileViewModel>
{
    public async ValueTask<AttachmentFileViewModel> Handle(GetAttachmentQuery query, CancellationToken cancellationToken)
    {
        Message? message = await context.Messages
            .Where(m => m.Id == query.MessageId)
            .FirstOrDefaultAsync(cancellationToken);

        Attachment? attachment = message?.Attachments?.FirstOrDefault(
            x => x.Id == query.AttachmentId
        );
        
        if (attachment == null)
            AttachmentNotFoundException.Throw(query.ChannelId, query.MessageId, query.AttachmentId);
        
        Stream stream = await attachmentsStore.GetFileStreamAsync(
            new AttachmentPath(query.ChannelId, query.MessageId, query.AttachmentId),
            cancellationToken
        );

        if (stream == Stream.Null)
            throw new InvalidOperationException($"Existing attachment not found {query.ChannelId}/{query.MessageId}/{query.AttachmentId}");

        return new AttachmentFileViewModel(
            attachment.FileName,
            attachment.FileName,
            attachment.MediaType,
            stream
        );
    }
}