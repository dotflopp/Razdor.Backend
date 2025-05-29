using Microsoft.Extensions.Caching.Hybrid;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Authorization;
using Razdor.Communities.Services.Exceptions;

namespace Razdor.Communities.Infrastructure.Authorization;

public class CachedCommunityPermissionsAccessor(
    HybridCache cache,
    ICommunitiesRepository communities,
    ICommunityMembersRepository members
) : ICommunityPermissionsAccessor
{

    public async Task<UserPermissions> GetMemberPermissionsAsync(ulong userId, ulong communityId, CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync(
            GenerateKey(userId, communityId),
            cancel => CalculatePermissionsAsync(userId, communityId, cancel),
            cancellationToken:cancellationToken
        );
    }
    public string GenerateKey(ulong userId, ulong communityId)
    {
        return string.Join("/", "cached-community-permissions", userId, communityId);
    }

    private async ValueTask<UserPermissions> CalculatePermissionsAsync(ulong userId, ulong communityId, CancellationToken cancellationToken)
    {
        CommunityMember? member = await members.FindAsync(communityId, userId, cancellationToken);

        if (member == null)
            CommunityMemberNotFoundException.Throw(userId, communityId);

        Community? community = await communities.FindAsync(communityId, cancellationToken);

        if (community == null)
            throw new InvalidOperationException($"There is a CommunityMember({userId}) for whom there is no Community({communityId})");

        return community.GetPermissions(member);
    }
}