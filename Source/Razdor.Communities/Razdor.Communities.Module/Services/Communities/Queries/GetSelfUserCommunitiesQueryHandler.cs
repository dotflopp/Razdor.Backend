using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Services.DataAccess;
using Razdor.Communities.Services.Services.Communities.ViewModels;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Services.Services.Communities.Queries;

public sealed class GetSelfUserCommunitiesQueryHandler(
    CommunityDataContext context,
    IRequestSenderContextAccessor sender
) : IQueryHandler<GetSelfUserCommunitiesQuery, IEnumerable<CommunityViewModel>>
{
    public async ValueTask<IEnumerable<CommunityViewModel>> Handle(
        GetSelfUserCommunitiesQuery command,
        CancellationToken cancellationToken
    )
    {
        List<ulong> userCommunityIds = await context.CommunityMembers.AsNoTracking()
            .Where(x => x.UserId == sender.User.Id)
            .Select(x => x.CommunityId)
            .ToListAsync(cancellationToken);

        List<Community> communities = await context.Communities.AsNoTracking()
            .Where(x => userCommunityIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        return communities.Select(CommunityViewModel.From);
    }
}