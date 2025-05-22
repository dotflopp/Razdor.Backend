using Razdor.Shared.Domain.Repository;

namespace Razdor.Communities.Domain;

public interface ICommunitiesRepository : IUnitOfWorkRepository<Community>
{
    Community Add(Community community);
    Community Update(Community community);
}