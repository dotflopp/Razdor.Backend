using Mediator;
using Microsoft.Extensions.Caching.Hybrid;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.InternalQueries;
using Razdor.Communities.PublicEvents.Events;
using Razdor.Shared.Module.Authorization;
using static Razdor.Communities.Infrastructure.Authorization.TagsHelper;

namespace Razdor.Communities.Infrastructure.Authorization;


public class CachedChannelPermissionsAccessor(
    HybridCache cache,
    ICommunitiesModule module
) : IChannelPermissionsAccessor, INotificationHandler<MemberChangedPublicEvent> {
    
    public async Task<UserPermissions> GetMemberPermissionsAsync(ulong userId, ulong channelId, CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync(
            key: ChannelPermissionsKey(channelId, userId),
            factory: async (cancel) => await module.ExecuteQueryAsync(
                new GetChannelMemberPermissions(channelId, userId), cancel
            ),
            tags: [UserTag(userId)],
            cancellationToken: cancellationToken
        );
    }
    
    public async ValueTask Handle(MemberChangedPublicEvent notification, CancellationToken cancellationToken)
    {
        if (!notification.Changes.HasFlag(MemberProperties.Roles))
            return;

        await cache.RemoveByTagAsync(UserTag(notification.UserId));
    }
}