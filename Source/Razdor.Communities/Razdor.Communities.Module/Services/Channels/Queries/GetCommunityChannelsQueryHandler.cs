using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Services.Channels.ViewModels;

namespace Razdor.Communities.Module.Services.Channels.Queries;

public class GetCommunityChannelsQueryHandler(
    CommunitiesDbContext context
) : IQueryHandler<GetCommunityChannelsQuery, IEnumerable<ChannelViewModel>>
{
    public async ValueTask<IEnumerable<ChannelViewModel>> Handle(GetCommunityChannelsQuery query, CancellationToken cancellationToken)
    {
        List<CommunityChannel> channels = await context.Channels.Where(
            x => x.CommunityId == query.CommunityId
        ).ToListAsync();

        return channels.Select(ChannelViewModel.From);
    }
}