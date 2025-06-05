using System.Buffers.Text;
using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Invites;
using Razdor.Communities.Module.DataAccess;
using Razdor.Communities.Module.Exceptions;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Communities.Module.Services.Invites.ViewModels;
using Razdor.Shared.Module;
using Razdor.Shared.Module.DataAccess;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Services.Invites.Commands;

public sealed class CreateInviteCommandHandler(
    TimeProvider timeProvider,
    IRequestSenderContextAccessor senderContext,
    ICommunitiesRepository communities,
    IInvitesRepository invites,
    SnowflakeGenerator snowflakeGenerator,
    UnitOfWork<CommunitiesDbContext> unitOfWork
) : ICommandHandler<CreateInviteCommand, InvitePreviewModel>
{
    public async ValueTask<InvitePreviewModel> Handle(CreateInviteCommand command, CancellationToken cancellationToken)
    {
        Community community = await communities.FindAsync(command.CommunityId, cancellationToken);
        
        ulong snowflakeId = snowflakeGenerator.Next();
        string strId = Base64Url.EncodeToString(
            BitConverter.GetBytes(snowflakeId)
        );

        var invite = Invite.Create(
            strId,
            senderContext.User.Id,
            community.Id,
            command.LifeTime,
            timeProvider
        );

        invites.Add(invite);

        await unitOfWork.SaveEntitiesAsync();

        return InvitePreviewModel.From(invite);
    }
}