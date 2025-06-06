using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Communities.Module.Services.Members.Queries;

public class GetCommunityMemberQueryHandler(
    CommunitiesDbContext context,
    ICommunityUserDataAccessor dataAccessor     
) : IQueryHandler<GetCommunityMemberQuery, CommunityMemberPreviewModel>
{
    public async ValueTask<CommunityMemberPreviewModel> Handle(GetCommunityMemberQuery query, CancellationToken cancellationToken)
    { 
        CommunityMember? member = await context.CommunityMembers.AsNoTracking()
            .Where(x => x.CommunityId == query.CommunityId && x.UserId == query.UserId)
            .FirstOrDefaultAsync();
        
        ResourceNotFoundException.ThrowIfNull(member);
        
        UserDataViewModel profile = await dataAccessor.FillAsync(member.UserId, new MemberProfile(null, null), cancellationToken);

        return CommunityMemberPreviewModel.From(member, profile);
    }
}