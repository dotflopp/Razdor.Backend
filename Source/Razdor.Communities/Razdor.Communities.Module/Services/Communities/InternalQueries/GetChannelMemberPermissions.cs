using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;

namespace Razdor.Communities.Module.Services.Communities.InternalQueries;

public record GetChannelMemberPermissions(
    ulong CommunityId,
    ulong ChannelId,
    ulong UserId
) : ICommunitiesQuery<UserPermissions>;