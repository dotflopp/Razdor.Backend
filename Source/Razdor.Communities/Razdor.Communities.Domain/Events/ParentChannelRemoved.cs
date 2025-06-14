using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels.Events;

public record ParentChannelRemoved(
    ulong ChannelId, ulong ParentId
): IDomainEvent; 