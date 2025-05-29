using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Services.DataAccess;

public class CommunityChannelsRepository(
    UnitOfWork<CommunityDataContext> unitOfWork,
    CommunityDataContext _context
) : ICommunityChannelsRepository
{
    public IUnitOfWork UnitOfWork => unitOfWork;
    public CommunityChannel Add(CommunityChannel communityChannel)
    {
        EntityEntry<CommunityChannel> entry = _context.Add(communityChannel);
        return entry.Entity;
    }
}