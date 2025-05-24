using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.DataAccess;
using Razdor.Shared.Domain.Rules;
using Razdor.Shared.Module;
using Razdor.Shared.Module.DataAccess;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Services.Communities.Commands;

public class CreateCommunityCommandHandler(
    UnitOfWork<CommunityDataContext> unitOfWork,
    ICommunitiesRepository communities,
    ICommunityMembersRepository communityMembers,
    IRequestSenderContext sender,
    SnowflakeGenerator snowflakeGenerator,
    TimeProvider timeProvider
): ICommandHandler<CreateCommunityCommand, CommunityViewModel>
{
    public async ValueTask<CommunityViewModel> Handle(CreateCommunityCommand command, CancellationToken cancellationToken)
    {
        Community community = Community.CreateNew(
            snowflakeGenerator.Next(),
            sender.User.Id,
            command.Name,
            null,
            null
        );
         
        CommunityMember member = CommunityMember.CreateNew(
            sender.User.Id,
            community.Id,
            timeProvider
        );
        
        communities.Add(community);
        communityMembers.Add(member);

        await unitOfWork.SaveEntitiesAsync(cancellationToken);
        return CommunityViewModel.From(community);
    }
}