using Microsoft.Extensions.Caching.Hybrid;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.InternalQueries;
using Razdor.Communities.Module.Services.Members;
using Razdor.Communities.Module.Services.Members.ViewModels;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Users.Queries;
using Razdor.Identity.Module.Users.ViewModels;

namespace Razdor.Communities.Infrastructure;

public class CachedUserProfileFiler(
    HybridCache cache,
    IIdentityModule identityModule
) : IUserProfileFiller {
    public async Task<MemberProfileViewModel> FillAsync(ulong userId, MemberProfile profile, CancellationToken cancellationToken)
    {
        MemberProfileViewModel userProfile = await cache.GetOrCreateAsync(
            key: ProfileCacheKey(userId),
            factory: async (cancel) => await GetUserProfileAsync(userId, cancel),
            cancellationToken: cancellationToken
        );
        
        return userProfile with {
            Avatar        = profile.Avatar?.SourceUrl ?? userProfile.Avatar,
            Nickname      = profile.Nickname ?? userProfile.Nickname,
        }; 
    }
    
    private string ProfileCacheKey(ulong userId)
    {
        return $"profiles/{userId}";
    }

    private async Task<MemberProfileViewModel> GetUserProfileAsync(ulong userId, CancellationToken cancellationToken)
    {
        UserPreviewModel user = await identityModule.ExecuteQueryAsync(new GetUserQuery(userId));
        return new MemberProfileViewModel(user.IdentityName, user.Nickname, user.Avatar);
    }
}