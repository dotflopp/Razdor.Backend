using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.DataAccess;
using Razdor.Communities.Services.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Services.Services.Communities.Queries;

public class GetCommunityQueryHandler(
    IRequestSenderContextAccessor sender,
    CommunityDataContext context
): IQueryHandler<GetCommunityQuery, CommunityViewModel>
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