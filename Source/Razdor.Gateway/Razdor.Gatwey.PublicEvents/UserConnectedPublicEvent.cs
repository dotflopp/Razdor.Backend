using Razdor.Shared.IntegrationEvents;

namespace Razdor.Gateways.PublicEvents;

public record UserConnectedPublicEvent(ulong UserId) : IPublicEvent;