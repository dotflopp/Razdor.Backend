using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Exceptions;
using Razdor.Communities.PublicEvents.ViewModels.Communities;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Communities.Module.Services.Communities.Queries;

public class GetCommunityQueryHandler(
    CommunitiesDbContext context
) : IQueryHandler<GetCommunityQuery, CommunityViewModel>
{
    public async ValueTask<CommunityViewModel> Handle(GetCommunityQuery query, CancellationToken cancellationToken)
    {
        Community? community = await context.Communities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.CommunityId, cancellationToken);

        ResourceNotFoundException.ThrowIfNull(community, query.CommunityId);
        
        return CommunityViewModel.From(community);
    }
}