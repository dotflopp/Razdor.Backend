using Mediator;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Messages.Module.Services.Commands;

public class SendMessageCommandHandler(
    SnowflakeGenerator snowflake,
    IRequestSenderContextAccessor sender,
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

        ulong messageId = snowflake.Next();
        List<AttachmentMeta> attachments = [];
        
        AttachmentPath path = new AttachmentPath(command.ChannelId, messageId, 0);
        await foreach (var attachment in command.Files.WithCancellation(cancellationToken))
        {
            path.AttachmentId = snowflake.Next();
            bool isSuccess = await store.UploadFileAsync(
                path.AsString(), attachment.ContentType, attachment.Stream, cancellationToken
            );

            if (!isSuccess)
                MediaFileNotUploadedException.Throw();
            
            attachments.Add(
                new AttachmentMeta(
                    path.AttachmentId,
                    attachment.FileName,
                    path.AsString(),
                    attachment.ContentType,
                    attachment.Stream.Length
                )    
            );
        }
        
        Message message = Message.CreateNew(
            messageId,
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