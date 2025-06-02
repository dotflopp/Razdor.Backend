using Mediator;
using Razdor.Messaging.Domain;
using Razdor.Messaging.Module.Services.Commands.ViewModels;
using Razdor.Shared.Module;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Messaging.Module.Services.Commands;

public class SendMessageCommandHandler(
    SnowflakeGenerator snowflake,
    IRequestSenderContextAccessor sender,
    IMessagesRepository messageses,
    TimeProvider timeProvider
): ICommandHandler<SendMessageCommand, MessageViewModel>
{

    public async ValueTask<MessageViewModel> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        MessageReference? reference = (command.Reference != null)
            ? new MessageReference(command.Reference.ChannelId, command.Reference.MessageId)
            : null;
        
        Message message = Message.CreateNew(
            snowflake.Next(),
            sender.User.Id,
            command.ChannelId,
            command.Text,
            timeProvider,
            reference,
            command.Embed,
            null
        );
        
        messageses.Add(message);
        await messageses.UnitOfWork.SaveEntitiesAsync();
        
        return MessageViewModel.From(message);
    }
}