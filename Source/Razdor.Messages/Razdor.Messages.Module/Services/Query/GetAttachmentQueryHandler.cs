using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.DataAccess;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.Media;

namespace Razdor.Messages.Module.Services.Query;

public class GetAttachmentQueryHandler(
    IFileStore store,
    MessagesDbContext context
) : IQueryHandler<GetAttachmentQuery, MediaFileViewModel>
{
    public async ValueTask<MediaFileViewModel> Handle(GetAttachmentQuery query, CancellationToken cancellationToken)
    {
        Message? message = await context.Messages
            .Where(m => m.Id == query.MessageId)
            .FirstOrDefaultAsync(cancellationToken);

        AttachmentMeta? attachment = message?.Attachments?.FirstOrDefault(
            x => x.Id == query.AttachmentId
        );
        
        if (attachment == null)
            MediaFileNotFoundException.Throw();

        AttachmentPath path = new AttachmentPath(query.ChannelId, query.MessageId, query.AttachmentId);
        return await MediaHelper.GetMediaFileAsync(store, path, attachment, cancellationToken);
    }
}