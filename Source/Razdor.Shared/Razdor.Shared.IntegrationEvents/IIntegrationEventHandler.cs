using Mediator;

namespace Razdor.Shared.IntegrationEvents;

public interface IIntegrationEventHandler<in TNotification> 
    : INotificationHandler<TNotification> where TNotification : IIntegrationEvent;