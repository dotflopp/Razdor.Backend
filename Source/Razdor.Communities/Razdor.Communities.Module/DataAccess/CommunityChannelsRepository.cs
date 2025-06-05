using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Communities.Module.DataAccess;

public class CommunityChannelsRepository(
    UnitOfWork<CommunitiesDbContext> unitOfWork,
    CommunitiesDbContext _context
) : ICommunityChannelsRepository
{
    public IUnitOfWork UnitOfWork => unitOfWork;
    public CommunityChannel Add(CommunityChannel communityChannel)
    {
        EntityEntry<CommunityChannel> entry = _context.Add(communityChannel);
        return entry.Entity;
    }
    public async Task<CommunityChannel> FindAsync(ulong channelId, CancellationToken cancellationToken = default)
    {
        CommunityChannel? channel = await _context.Channels.FirstOrDefaultAsync(
            channel => channel.Id == channelId, 
            cancellationToken
        );
        ResourceNotFoundException.ThrowIfNull(channel, channelId);
        
        return channel;
    }
    
    public void Delete(CommunityChannel channel)
    {
        _context.Channels.Remove(channel);
    }
    
    public async Task<List<CommunityChannel>> GetChildsAsync(ulong parentId, CancellationToken cancellationToken)
    {
        return await _context.Channels
            .Where(channel => channel.ParentId == parentId)
            .ToListAsync(cancellationToken);
    }
}