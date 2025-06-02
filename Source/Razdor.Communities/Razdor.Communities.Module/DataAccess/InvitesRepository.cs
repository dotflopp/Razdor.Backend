using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain.Invites;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Module.DataAccess;

public class InvitesRepository(
    UnitOfWork<CommunitiesDbContext> unitOfWork,
    CommunitiesDbContext context
) : IInvitesRepository
{
    public IUnitOfWork UnitOfWork => unitOfWork;
    public Invite Add(Invite invite)
    {
        return context.Invites.Add(invite).Entity;
    }

    public void Remove(Invite invite)
    {
        context.Invites.Remove(invite);
    }

    public Task<Invite?> FindAsync(string id)
    {
        return context.Invites.FirstOrDefaultAsync(x => x.Id == id);
    }
}