using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Communities.Domain.Members;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Services.DataAccess;

public class CommunityMembersRepository(
    CommunityDataContext context,
    UnitOfWork<CommunityDataContext> unitOfWork
): ICommunityMembersRepository
{
    private readonly CommunityDataContext _context = context;
    public IUnitOfWork UnitOfWork => unitOfWork;
    public CommunityMember Add(CommunityMember communityMember)
    {
        EntityEntry<CommunityMember> entry = _context.Add(communityMember);
        return entry.Entity;
    }
}