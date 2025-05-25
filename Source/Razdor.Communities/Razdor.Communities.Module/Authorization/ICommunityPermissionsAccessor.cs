using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Services.Authorization;

public interface ICommunityPermissionsAccessor
{
    Task<UserPermissions> GetMemberPermissionsAsync(ulong userId, ulong communityId, CancellationToken cancellationToken = default);
}