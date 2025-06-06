using Mediator;

namespace Razdor.Shared.IntegrationEvents;

public interface IPublicEvent : INotification;

public interface IPublicEvent<TPayload> : IPublicEvent
{
    public TPayload Payload { get; }
};