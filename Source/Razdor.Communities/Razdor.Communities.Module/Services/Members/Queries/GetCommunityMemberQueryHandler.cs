using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Exceptions;
using Razdor.Communities.Module.Services.Members.ViewModels;

namespace Razdor.Communities.Module.Services.Members;

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
        
        if (member == null)
            CommunityMemberNotFoundException.Throw(query.CommunityId, query.UserId);
        
        UserDataViewModel profile = await dataAccessor.FillAsync(member.UserId, new MemberProfile(null, null), cancellationToken);

        return CommunityMemberPreviewModel.From(member, profile);
    }
}