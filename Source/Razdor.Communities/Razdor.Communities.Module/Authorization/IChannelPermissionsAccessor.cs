using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Module.Authorization;

public interface IChannelPermissionsAccessor
{
    Task<UserPermissions> GetMemberPermissionsAsync(ulong communityId, ulong userId, ulong channelId, CancellationToken cancellationToken = default);
}