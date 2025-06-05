using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Invites;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Rules;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Exceptions;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Communities.Module.Services.Invites.ViewModels;
using Razdor.Shared.Domain.Rules;
using Razdor.Shared.Module.DataAccess;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Services.Invites.Commands;

public sealed class AcceptInviteCommandHandler(
    IInvitesRepository invites,
    ICommunitiesRepository communities,
    ICommunityMembersRepository members,
    UnitOfWork<CommunitiesDbContext> unitOfWork,
    IRequestSenderContext sender,
    TimeProvider timeProvider
) : ICommandHandler<AcceptInviteCommand, InvitePreviewModel>
{
    public async ValueTask<InvitePreviewModel> Handle(AcceptInviteCommand command, CancellationToken cancellationToken)
    {
        Invite invite = await invites.FindAsync(command.InviteId);

        if (!await communities.ContainsAsync(invite.CommunityId))
            ExceptionHelper.ThrowInviteWithoutCommunity(invite);
        
        await RuleValidationHelper.ThrowIfBrokenAsync(
            new CantJoinSameCommunityTwice(members, invite.CommunityId, sender.User.Id)
        );

        var communityMember = CommunityMember.CreateNew(sender.User.Id, invite.CommunityId, timeProvider);
        members.Add(communityMember);

        invite.UsesCount++;
        
        await unitOfWork.SaveEntitiesAsync();
        return InvitePreviewModel.From(invite);
    }
}