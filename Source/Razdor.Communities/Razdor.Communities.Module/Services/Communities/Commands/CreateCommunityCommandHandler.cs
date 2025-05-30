﻿using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Services.DataAccess;
using Razdor.Communities.Services.Services.Communities.ViewModels;
using Razdor.Shared.Module;
using Razdor.Shared.Module.DataAccess;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Services.Services.Communities.Commands;

public sealed class CreateCommunityCommandHandler(
    UnitOfWork<CommunityDataContext> unitOfWork,
    ICommunitiesRepository communities,
    ICommunityMembersRepository communityMembers,
    IRequestSenderContextAccessor senderContext,
    SnowflakeGenerator snowflakeGenerator,
    TimeProvider timeProvider
) : ICommandHandler<CreateCommunityCommand, CommunityViewModel>
{
    public async ValueTask<CommunityViewModel> Handle(CreateCommunityCommand command, CancellationToken cancellationToken)
    {
        var community = Community.CreateNew(
            snowflakeGenerator.Next(),
            senderContext.User.Id,
            command.Name,
            null,
            null
        );

        var member = CommunityMember.CreateNew(
            senderContext.User.Id,
            community.Id,
            timeProvider
        );

        communities.Add(community);
        communityMembers.Add(member);

        await unitOfWork.SaveEntitiesAsync(cancellationToken);
        return CommunityViewModel.From(community);
    }
}