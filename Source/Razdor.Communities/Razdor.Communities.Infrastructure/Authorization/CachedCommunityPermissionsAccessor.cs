using Microsoft.Extensions.Caching.Hybrid;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.InternalQueries;
using Razdor.Shared.Module.Authorization;
using static Razdor.Communities.Infrastructure.Authorization.TagsHelper;

namespace Razdor.Communities.Infrastructure.Authorization;

public class CachedCommunityPermissionsAccessor(
    HybridCache cache,
    ICommunitiesModule module
) : ICommunityPermissionsAccessor 
{
    public async Task<UserPermissions> GetMemberPermissionsAsync(ulong communityId, ulong userId, CancellationToken cancellationToken)
    {
         (UserPermissions permissons, uint priority) = await cache.GetOrCreateAsync(
            key: CommunityPermissionsKey(communityId, userId), 
            factory: async cancel => await module.ExecuteQueryAsync(
                new GetCommunityMemberPermissions(communityId, userId), cancel
            ),
            cancellationToken: cancellationToken
        );

        return permissons;
    }
    
    public async Task<(UserPermissions permissions, uint Priority)> GetMemberPermissionsAndPriorityAsync(ulong communityId, ulong userId, CancellationToken cancellationToken = default)
    {
        return await cache.GetOrCreateAsync(
            key: CommunityPermissionsKey(communityId, userId), 
            factory: async cancel => await module.ExecuteQueryAsync(
                new GetCommunityMemberPermissions(communityId, userId), cancel
            ),
            cancellationToken: cancellationToken
        );
    }
}