using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels.Events;

public record OverwritePermissionsChanged(
    ulong ChannelId,
    IReadOnlyCollection<(Overwrite, Overwrite)>? ChangedPermissions,
    IReadOnlyCollection<Overwrite>? RemovedPermissions,
    IReadOnlyCollection<Overwrite>? AddedPermissions
) : IDomainEvent;