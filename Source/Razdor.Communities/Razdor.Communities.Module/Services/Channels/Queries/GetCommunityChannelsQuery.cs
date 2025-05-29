using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Authorization;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Channels.ViewModels;

namespace Razdor.Communities.Services.Services.Channels.Queries;

public record GetCommunityChannelsQuery(
    ulong CommunityId
) : ICommunitiesQuery<IEnumerable<ChannelViewModel>>, IRequiredCommunityPermissionsMessage
{
    public UserPermissions RequiredPermissions => UserPermissions.None;
}