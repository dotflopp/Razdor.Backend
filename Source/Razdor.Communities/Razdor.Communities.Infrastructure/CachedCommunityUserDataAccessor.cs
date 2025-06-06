﻿using Mediator;
using Microsoft.Extensions.Caching.Hybrid;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.InternalQueries;
using Razdor.Communities.Module.Services.Members;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Services.Users.Queries;
using Razdor.Identity.Module.Services.Users.ViewModels;
using Razdor.Identity.PublicEvents.Event;

namespace Razdor.Communities.Infrastructure;

public class CachedCommunityUserDataAccessor(
    HybridCache cache,
    IIdentityModule identityModule
) : ICommunityUserDataAccessor, INotificationHandler<UserChangedPublicEvent> {
    public async Task<UserDataViewModel> FillAsync(ulong userId, MemberProfile profile, CancellationToken cancellationToken)
    {
        UserDataViewModel userProfile = await cache.GetOrCreateAsync(
            key: UserDataCacheKey(userId),
            factory: async (cancel) => await GetUserProfileAsync(userId, cancel),
            cancellationToken: cancellationToken
        );
        
        return userProfile with {
            Avatar        = profile.Avatar?.SourceUrl ?? userProfile.Avatar,
            Nickname      = profile.Nickname ?? userProfile.Nickname,
        }; 
    }
    
    private string UserDataCacheKey(ulong userId)
    {
        return $"users/{userId}";
    }

    private async Task<UserDataViewModel> GetUserProfileAsync(ulong userId, CancellationToken cancellationToken)
    {
        UserPreviewModel user = await identityModule.ExecuteQueryAsync(new GetUserQuery(userId));
        return new UserDataViewModel(user.IdentityName, user.Nickname, user.Avatar, (CommunicationStatus)user.Status);
    }
    
    public async ValueTask Handle(UserChangedPublicEvent notification, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(UserDataCacheKey(notification.UserId));
    }
}