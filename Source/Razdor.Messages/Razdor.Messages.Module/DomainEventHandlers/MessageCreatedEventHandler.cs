using Mediator;
using Razdor.Messages.Domain.Events;
using Razdor.Messages.PublicEvents;
using Razdor.Messages.PublicEvents.Events;
using Razdor.Messages.PublicEvents.ViewModels;
using Razdor.Shared.IntegrationEvents;

namespace Razdor.Messages.Module.NotificatoinHandlers;

public class MessageCreatedEventHandler(
    IEventBus eventBus    
): INotificationHandler<MessageCreatedEvent>
{
    public async ValueTask Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.Publish(
            new MessageCreatedPublicEvent(MessageViewModel.From(notification.Message))
        );
    }
}