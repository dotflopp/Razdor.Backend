using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Services.Communities.Queries;

public sealed class GetSelfUserCommunitiesQueryHandler(
    CommunitiesDBContext context,
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