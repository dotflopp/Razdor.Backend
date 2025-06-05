using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels.Events;

public record OverwritePermissionsChanged(
    ulong ChannelId,
    List<Overwrite> NewPermissions
) : IDomainEvent;