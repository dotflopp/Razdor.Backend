using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Exceptions;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Services.Members.Commands;

public class ChangeMemberRolesCommandHandler(
    ICommunitiesRepository communities,
    ICommunityMembersRepository members,
    IRequestSenderContextAccessor sender,
    ICommunityPermissionsAccessor permisionsAccessor
): ICommandHandler<ChangeMemberRolesCommand>
{

    public async ValueTask<Unit> Handle(ChangeMemberRolesCommand command, CancellationToken cancellationToken)
    {
        Community community = await communities.FindAsync(command.CommunityId, cancellationToken)
            ?? throw new InvalidOperationException("Authorized access to a non-existent community");
        
        CommunityMember? member = await members.FindAsync(command.CommunityId, command.UserId, cancellationToken)
            ??  throw new InvalidOperationException("Authorized access to a non-existent member");
        
        (UserPermissions permissions, uint userPriority) = await permisionsAccessor.GetMemberPermissionsAndPriorityAsync(community.Id, member.UserId);

        member.RoleIds.Except(command.Roles);
        throw new NotImplementedException();
    }
}