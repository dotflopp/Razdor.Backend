using Razdor.Shared.IntegrationEvents;

namespace Razdor.Gateways.PublicEvents;

public record UserDisconnectedPublicEvent(ulong UserId) : IPublicEvent;