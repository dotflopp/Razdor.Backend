using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.DataAccess;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Services.Communities.Commands;

public class CreateCommunityCommandHandler(
    CommunityDataContext context,
    IRequestSenderContext sender,
    SnowflakeGenerator snowflakeGenerator
): ICommandHandler<CreateCommunityCommand, CommunityViewModel>
{
    public async ValueTask<CommunityViewModel> Handle(CreateCommunityCommand command, CancellationToken cancellationToken)
    {
        if (!sender.IsAuthenticated)
            ExceptionsHelper.ThrowUnauthenticatedException();

        ulong communityId = snowflakeGenerator.Next();
        
        Community community = new Community(
            communityId,
            sender.User.Id,
            command.Name,
            null,
            null,
            CommunityNotificationPolicy.All,
            [new EveryoneRole(communityId, EveryoneRole.InitialPermissions, 1)]
        );
        
        context.Add(community);

        CommunityUser owner = new CommunityUser(
            sender.User.Id,
            community.Id,
            true,
            VoiceState.Default,
            null,
            null,
            community.Roles.ToList(),
            DateTimeOffset.Now
        );
        
        await context.SaveChangesAsync();
        
        return CommunityViewModel.From(community);
    }
}