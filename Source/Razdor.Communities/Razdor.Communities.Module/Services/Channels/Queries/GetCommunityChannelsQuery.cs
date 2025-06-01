using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Channels.ViewModels;

namespace Razdor.Communities.Module.Services.Channels.Queries;

public record GetCommunityChannelsQuery(
    ulong CommunityId
) : ICommunitiesQuery<IEnumerable<ChannelViewModel>>, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.None;
}