using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Module.DataAccess;

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
    public async Task<CommunityChannel?> FindAsync(ulong communityId, ulong channelId, CancellationToken cancellationToken = default)
    {
        return await _context.Channels.FirstOrDefaultAsync(
            channel => channel.Id == channelId && channel.CommunityId == communityId, 
            cancellationToken
        );
    }
}