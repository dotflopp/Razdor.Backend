﻿using Razdor.Communities.Domain.Permissions;

namespace Razdor.Shared.Module.Authorization;

public interface ICommunityPermissionsAccessor
{
    Task<UserPermissions> GetMemberPermissionsAsync(ulong communityId, ulong userId, CancellationToken cancellationToken = default);
    Task<(UserPermissions permissions, uint Priority)> GetMemberPermissionsAndPriorityAsync(
        ulong communityId, ulong userId, CancellationToken cancellationToken = default
    );

}