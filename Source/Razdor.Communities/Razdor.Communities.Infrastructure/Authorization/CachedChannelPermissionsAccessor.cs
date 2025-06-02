using Microsoft.Extensions.Caching.Hybrid;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.InternalQueries;
using static Razdor.Communities.Infrastructure.Authorization.TagsHelper;

namespace Razdor.Communities.Infrastructure.Authorization;


public class CachedChannelPermissionsAccessor(
    HybridCache cache,
    ICommunityModule module
) : IChannelPermissionsAccessor {
    
    public async Task<UserPermissions> GetMemberPermissionsAsync(ulong userId, ulong channelId, CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync(
            key: ChannelPermissionsKey(channelId, userId),
            factory: async (cancel) => await module.ExecuteQueryAsync(
                new GetChannelMemberPermissions(channelId, userId), cancel
            ),
            cancellationToken: cancellationToken
        );
    }
}