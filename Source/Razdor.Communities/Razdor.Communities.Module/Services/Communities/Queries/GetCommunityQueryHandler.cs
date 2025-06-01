using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Exceptions;
using Razdor.Communities.Module.Services.Communities.ViewModels;

namespace Razdor.Communities.Module.Services.Communities.Queries;

public class GetCommunityQueryHandler(
    CommunityDataContext context
) : IQueryHandler<GetCommunityQuery, CommunityViewModel>
{
    public async ValueTask<CommunityViewModel> Handle(GetCommunityQuery query, CancellationToken cancellationToken)
    {
        Community? community = await context.Communities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.CommunityId, cancellationToken);

        if (community is null)
            CommunityNotFoundException.Throw(query.CommunityId);

        return CommunityViewModel.From(community);
    }
}