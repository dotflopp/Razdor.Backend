using Razdor.Communities.Domain.Permissions;

namespace Razdor.Shared.Module.Authorization;

public interface IChannelPermissionsAccessor
{
    Task<UserPermissions> GetMemberPermissionsAsync(ulong userId, ulong channelId, CancellationToken cancellationToken = default);
}