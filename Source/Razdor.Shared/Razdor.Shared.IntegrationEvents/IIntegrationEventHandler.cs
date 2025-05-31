using Mediator;

namespace Razdor.Shared.Module;

public interface IIntegrationEventHandler<in TNotification> 
    : INotificationHandler<TNotification> where TNotification : IIntegrationEvent;