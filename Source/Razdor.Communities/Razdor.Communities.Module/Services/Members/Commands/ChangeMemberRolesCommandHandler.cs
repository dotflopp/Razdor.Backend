using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Module.Exceptions;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Exceptions;
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
        
        IEnumerable<ulong> removedRoles = member.RoleIds.Except(command.Roles);
        IEnumerable<ulong> addedRoles = command.Roles.Except(member.RoleIds);
        
        uint removedRolesPriority = community.GetHighestPriority(removedRoles);
        uint addedRolesPriority = community.GetHighestPriority(addedRoles);
        
        uint highestPriority = Math.Min(removedRolesPriority, addedRolesPriority);
        if (userPriority > highestPriority)
            throw new NotEnoughRightsException("The user cannot change roles higher than his own in priority.");

        command.Roles.Sort();
        List<ulong> intersectionRoles = community
            .GetIntersectionRoles(command.Roles)
            .Select(x => x.Id)
            .ToList();
        
        member.UpdateRoles(intersectionRoles);
        await members.UnitOfWork.SaveEntitiesAsync();
        
        return Unit.Value;
    }
}