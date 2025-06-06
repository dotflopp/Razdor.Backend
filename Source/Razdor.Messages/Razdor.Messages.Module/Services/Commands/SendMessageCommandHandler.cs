using Mediator;
using Razdor.Messages.Domain;
using Razdor.Messages.PublicEvents.ViewModels;
using Razdor.Shared.Domain;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.Media;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Messages.Module.Services.Commands;

public class SendMessageCommandHandler(
    SnowflakeGenerator snowflake,
    IRequestSenderContext sender,
    IMessagesRepository messageses,
    TimeProvider timeProvider,
    IFileStore store
): ICommandHandler<SendMessageCommand, MessageViewModel>
{

    public async ValueTask<MessageViewModel> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        MessageReference? reference = (command.Reference != null)
            ? new MessageReference(command.Reference.ChannelId, command.Reference.MessageId)
            : null;

        
        AttachmentPath path = new AttachmentPath(command.ChannelId, snowflake.Next(), 0);
        List<AttachmentMeta> attachments = [];
        await foreach (var attachment in command.Files.WithCancellation(cancellationToken))
        {
            path.AttachmentId = snowflake.Next();
            MediaFileMeta meta = await store.UploadMediaFileAsync(path, attachment, cancellationToken);
            attachments.Add(
                new AttachmentMeta(path.AttachmentId, meta)    
            );
        }
        
        Message message = Message.CreateNew(
            path.MessageId,
            sender.User.Id,
            command.ChannelId,
            command.Text,
            timeProvider,
            reference,
            command.Embed,
            attachments
        );
        
        messageses.Add(message);
        await messageses.UnitOfWork.SaveEntitiesAsync();
        
        return MessageViewModel.From(message);
    }
}