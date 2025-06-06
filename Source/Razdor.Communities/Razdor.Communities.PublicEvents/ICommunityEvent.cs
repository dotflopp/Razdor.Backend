using Razdor.Shared.IntegrationEvents;

namespace Razdor.Communities.PublicEvents;

public interface ICommunityEvent : IPublicEvent
{
    public ulong CommunityId { get; }
}