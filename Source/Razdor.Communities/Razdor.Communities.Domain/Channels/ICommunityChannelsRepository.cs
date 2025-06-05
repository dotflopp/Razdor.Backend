using Razdor.Shared.Domain.Repository;

namespace Razdor.Communities.Domain.Channels;

public interface ICommunityChannelsRepository : IUnitOfWorkRepository<CommunityChannel>
{
    CommunityChannel Add(CommunityChannel communityChannel);
    Task<CommunityChannel> FindAsync(ulong channelId, CancellationToken cancellationToken = default);
}