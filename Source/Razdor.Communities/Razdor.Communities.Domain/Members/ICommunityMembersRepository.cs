using Razdor.Shared.Domain.Repository;

namespace Razdor.Communities.Domain.Members;

public interface ICommunityMembersRepository : IUnitOfWorkRepository<CommunityMember>
{
    CommunityMember Add(CommunityMember communityMember);
    Task<CommunityMember?> FindAsync(ulong communityId, ulong userId, CancellationToken cancellationToken = default);
    Task<bool> ContainsAsync(ulong communityId, ulong userId, CancellationToken cancellationToken = default);
}