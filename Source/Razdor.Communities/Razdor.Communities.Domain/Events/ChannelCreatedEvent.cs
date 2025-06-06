using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Events;

public record ChannelCreatedEvent(
    CommunityChannel Channel    
) : IDomainEvent;