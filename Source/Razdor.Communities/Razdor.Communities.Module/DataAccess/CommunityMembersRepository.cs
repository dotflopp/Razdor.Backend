using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Communities.Domain.Members;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Module.DataAccess;

public class CommunityMembersRepository(
    CommunitiesDBContext context,
    UnitOfWork<CommunitiesDBContext> unitOfWork
) : ICommunityMembersRepository
{
    private readonly CommunitiesDBContext _context = context;
    public IUnitOfWork UnitOfWork => unitOfWork;
    public CommunityMember Add(CommunityMember communityMember)
    {
        EntityEntry<CommunityMember> entry = _context.Add(communityMember);
        return entry.Entity;
    }

    public async Task<CommunityMember?> FindAsync(ulong communityId, ulong userId, CancellationToken cancellationToken = default)
    {
        return await _context.CommunityMembers.FindAsync(
            communityId, userId,
            cancellationToken
        );
    }

    public async Task<bool> ContainsAsync(ulong communityId, ulong userId, CancellationToken cancellationToken = default)
    {
        return await _context.CommunityMembers.AnyAsync(
            x => x.UserId == userId && x.CommunityId == communityId
        );
    }
}