using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Invites;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Exceptions;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Shared.Module.Exceptions;

namespace Razdor.Communities.Module.Services.Invites.Queries;

public class GetInviteCommandHandler(
    CommunitiesDbContext context    
): ICommandHandler<GetInviteCommand, InviteViewModel>
{
    public async ValueTask<InviteViewModel> Handle(GetInviteCommand command, CancellationToken cancellationToken)
    {
        Invite? invite = await context.Invites.AsNoTracking()
            .Where(x => x.Id == command.InviteId)
            .FirstOrDefaultAsync(cancellationToken);

        ResourceNotFoundException.ThrowIfNull(invite, command.InviteId);
        
        Community? community = await context.Communities.AsNoTracking()
            .Where(x => x.Id == invite.CommunityId)
            .FirstOrDefaultAsync();

        ResourceNotFoundException.ThrowIfNull(community, invite.CommunityId);

        return InviteViewModel.From(invite, community);
    }
}