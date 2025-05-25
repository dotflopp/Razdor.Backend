using Razdor.Shared.Domain.Repository;

namespace Razdor.Communities.Domain.Invites;

public interface IInvitesRepository : IUnitOfWorkRepository<Invite>
{
    Invite Add(Invite invite);
    void Remove(Invite invite);
    Task<Invite?> FindAsync(string id);
}