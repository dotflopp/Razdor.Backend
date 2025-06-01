using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Module.Authorization;

public interface ICommunityPermissionsAccessor
{
    Task<UserPermissions> GetMemberPermissionsAsync(ulong communityId, ulong userId, CancellationToken cancellationToken = default);
}