using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Invites;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Rules;
using Razdor.Communities.Services.DataAccess;
using Razdor.Communities.Services.Exceptions;
using Razdor.Communities.Services.UseCases.Invites.Commands;
using Razdor.Shared.Domain.Rules;
using Razdor.Shared.Module.DataAccess;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Services.Communities.Commands;

public class AcceptInviteCommandHandler(
    IInvitesRepository invites,
    ICommunitiesRepository communities,
    ICommunityMembersRepository members,
    UnitOfWork<CommunityDataContext> unitOfWork,
    IRequestSenderContextAccessor sender,
    TimeProvider timeProvider
): ICommandHandler<AcceptInviteCommand>
{
    public async ValueTask<Unit> Handle(AcceptInviteCommand command, CancellationToken cancellationToken)
    {
        Invite? invite = await invites.FindAsync(command.InviteId);

        if (invite is null)
            InviteNotFoundException.Throw(command.InviteId);

        if (!await communities.ContainsAsync(invite.CommunityId))
            throw new InvalidOperationException($"There is an Invite({invite.Id}) without a Community({invite.CommunityId})");

        await RuleValidationHelper.ThrowIfBrokenAsync(
            new CantJoinSameCommunityTwice(members, invite.CommunityId, sender.User.Id)  
        );
        
        CommunityMember communityMember = CommunityMember.CreateNew(sender.User.Id, invite.CommunityId, timeProvider);
        members.Add(communityMember);
        
        await unitOfWork.SaveEntitiesAsync();
        return Unit.Value;
    }
}