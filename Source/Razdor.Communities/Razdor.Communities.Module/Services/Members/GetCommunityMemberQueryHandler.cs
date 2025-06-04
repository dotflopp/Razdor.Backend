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
    IUserProfileFiller profileFiller     
) : IQueryHandler<GetCommunityMemberQuery, CommunityMemberPreviewModel>
{
    public async ValueTask<CommunityMemberPreviewModel> Handle(GetCommunityMemberQuery query, CancellationToken cancellationToken)
    { 
        CommunityMember? member = await context.CommunityMembers.AsNoTracking()
            .Where(x => x.CommunityId == query.CommunityId && x.UserId == query.UserId)
            .FirstOrDefaultAsync();
        
        if (member == null)
            CommunityMemberNotFoundException.Throw(query.CommunityId, query.UserId);

        MemberProfile emptyProfile = new MemberProfile(null, null);
        MemberProfileViewModel profile = await profileFiller.FillAsync(member.UserId, emptyProfile, cancellationToken);

        return CommunityMemberPreviewModel.From(member, profile);
    }
}