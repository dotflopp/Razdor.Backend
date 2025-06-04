using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Services.Members.ViewModels;

namespace Razdor.Communities.Module.Services.Members;

public class GetCommunityMembersQueryHandler(
    CommunitiesDbContext context,
    ICommunityUserDataAccessor dataAccessor 
) : IQueryHandler<GetCommunityMembersQuery, IEnumerable<CommunityMemberPreviewModel>>
{
    public async ValueTask<IEnumerable<CommunityMemberPreviewModel>> Handle(GetCommunityMembersQuery query, CancellationToken cancellationToken)
    {
        int count = query.UsersCount ?? 100;
        count = count < 0? 100: count;
        
        List<CommunityMember> members = await context.CommunityMembers.AsNoTracking()
            .Where(x => x.CommunityId == query.CommunityId)
            .Take(count)
            .OrderBy(x => x.UserId)
            .ToListAsync();

        List<UserDataViewModel> profiles = new (members.Count);

        MemberProfile emptyProfile = new MemberProfile(null, null);
        foreach (CommunityMember member in members)
        {
            UserDataViewModel profile = await dataAccessor.FillAsync(member.UserId, emptyProfile);
            profiles.Add(profile);
        }
        
        return members.Select((member, index) => CommunityMemberPreviewModel.From(member, profiles[index]));
    }
}