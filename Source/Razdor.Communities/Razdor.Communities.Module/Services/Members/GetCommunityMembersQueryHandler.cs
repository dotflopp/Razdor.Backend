using Mediator;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Module.Services.Members.ViewModels;

namespace Razdor.Communities.Module.Services.Members;

public class GetCommunityMembersQueryHandler : IQueryHandler<GetCommunityMembersQuery, IEnumerable<CommunityMemberPreviewModel>>
{
    public ValueTask<IEnumerable<CommunityMemberPreviewModel>> Handle(GetCommunityMembersQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}