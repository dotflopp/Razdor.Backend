using Razdor.Shared.Domain.Repository;

namespace Razdor.Communities.Domain.Members;

public interface ICommunityMembersRepository: IUnitOfWorkRepository<CommunityMember>
{
    CommunityMember Add(CommunityMember communityMember);
}