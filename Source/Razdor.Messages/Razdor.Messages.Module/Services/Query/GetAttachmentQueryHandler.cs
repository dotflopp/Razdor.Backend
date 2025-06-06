using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.DataAccess;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.Media;
using Razdor.Shared.Module.Media.Exceptions;

namespace Razdor.Messages.Module.Services.Query;

public class GetAttachmentQueryHandler(
    IFileStore store,
    MessagesDbContext context
) : IQueryHandler<GetAttachmentQuery, MediaFile>
{
    public async ValueTask<MediaFile> Handle(GetAttachmentQuery query, CancellationToken cancellationToken)
    {
        var message = await context.Messages
            .AsNoTracking()
            .Where(m => m.Id == query.MessageId)
            .Select(x => new { x.Attachments })
            .FirstOrDefaultAsync(cancellationToken);

        AttachmentMeta? attachment = message?.Attachments?.FirstOrDefault(
            x => x.Id == query.AttachmentId
        );
        
        if (attachment == null)
            MediaFileNotFoundException.Throw();

        AttachmentPath path = new AttachmentPath(query.ChannelId, query.MessageId, query.AttachmentId);
        return await store.GetMediaFileAsync(path, attachment, cancellationToken);
    }
}