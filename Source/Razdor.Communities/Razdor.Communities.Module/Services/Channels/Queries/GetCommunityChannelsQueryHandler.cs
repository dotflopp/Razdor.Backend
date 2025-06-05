using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Services.Channels.ViewModels;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Services.Channels.Queries;

public class GetCommunityChannelsQueryHandler(
    CommunitiesDbContext context,
    IChannelPermissionsAccessor channelPermissions,
    IRequestSenderContext sender
) : IQueryHandler<GetCommunityChannelsQuery, IEnumerable<ChannelViewModel>>
{
    public async ValueTask<IEnumerable<ChannelViewModel>> Handle(GetCommunityChannelsQuery query, CancellationToken cancellationToken)
    {
        List<CommunityChannel> channels = await context.Channels.Where(
            x => x.CommunityId == query.CommunityId
        ).ToListAsync();

        IEnumerable<Task<UserPermissions>> waitPermissions = channels.Select(channel => 
            channelPermissions.GetMemberPermissionsAsync(sender.User.Id, channel.Id)
        ).ToList();
        
        UserPermissions[] permissions = await Task.WhenAll(waitPermissions);
        
        return channels
            .Where((channel, index) => permissions[index].HasFlag(UserPermissions.ViewChannel))
            .Select(ChannelViewModel.From);
    }
}