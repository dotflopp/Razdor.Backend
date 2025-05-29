using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Services.DataAccess;
using Razdor.Communities.Services.Services.Channels.Commands;

namespace Razdor.Communities.Services.Services.Channels.Queries;

public class GetCommunityChannelsQueryHandler(
    CommunityDataContext context
): IQueryHandler<GetCommunityChannelsQuery, IEnumerable<ChannelViewModel>>
{
    public async ValueTask<IEnumerable<ChannelViewModel>> Handle(GetCommunityChannelsQuery query, CancellationToken cancellationToken)
    {
        List<CommunityChannel> channels = await context.Channels.Where(
            x => x.CommunityId == query.CommunityId
        ).ToListAsync();

        return channels.Select(ChannelViewModel.From);
    }
}