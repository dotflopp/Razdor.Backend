using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Channels.Queries;

public record GetCommunityChannelsQuery(
    ulong CommunityId
) : ICommunitiesQuery<IEnumerable<ChannelViewModel>>, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.None;
}