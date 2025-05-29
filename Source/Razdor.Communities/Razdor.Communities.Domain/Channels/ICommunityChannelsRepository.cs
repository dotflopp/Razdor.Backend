using Razdor.Shared.Domain.Repository;

namespace Razdor.Communities.Domain.Channels;

public interface ICommunityChannelsRepository : IUnitOfWorkRepository<CommunityChannel>
{
    CommunityChannel Add(CommunityChannel communityChannel);
}