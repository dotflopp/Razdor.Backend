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
        Message message = Message.CreateNew(
            snowflake.Next(),
            sender.User.Id,
            command.ChannelId,
            command.Text,
            timeProvider,
            new MessageReference(command.Reference.ChannelId, command.Reference.MessageId),
            command.Embed,
            null
        );
        
        messageses.Add(message);
        await messageses.UnitOfWork.SaveEntitiesAsync();
        
        return MessageViewModel.From(message);
    }
}