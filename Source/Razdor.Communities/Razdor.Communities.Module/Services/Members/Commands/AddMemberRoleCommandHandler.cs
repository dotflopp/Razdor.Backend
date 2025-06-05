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

public class AddMemberRoleCommandHandler(
    ICommunitiesRepository communities,
    ICommunityMembersRepository members,
    IRequestSenderContext sender,
    ICommunityPermissionsAccessor permisionsAccessor
): ICommandHandler<AddMemberRoleCommand>
{

    public async ValueTask<Unit> Handle(AddMemberRoleCommand command, CancellationToken cancellationToken)
    {
        Community community = await communities.FindAsync(command.CommunityId, cancellationToken);
        CommunityMember changedMember = await members.FindAsync(command.CommunityId, command.UserId, cancellationToken);
        
        var (senderPermissions, senderPriority) = await permisionsAccessor.GetMemberPermissionsAndPriorityAsync(
            community.Id, sender.User.Id
        );

        Role? addedRole = community.FindRoleById(command.RoleId);
        ResourceNotFoundException.ThrowIfNull(addedRole, (command.CommunityId, command.RoleId));
        
        if (senderPriority > addedRole.Priority)
            NotEnoughRightsException.ThrowResourceHasHigherPriority();
        
        changedMember.AddRole(addedRole);
        await members.UnitOfWork.SaveEntitiesAsync();
        
        // IEnumerable<ulong> removedRoles = member.RoleIds.Except(command.Roles);
        // IEnumerable<ulong> addedRoles = command.Roles.Except(member.RoleIds);
        //
        // uint removedRolesPriority = community.GetHighestPriority(removedRoles);
        // uint addedRolesPriority = community.GetHighestPriority(addedRoles);
        //
        // uint highestPriority = Math.Min(removedRolesPriority, addedRolesPriority);
        // if (userPriority > highestPriority)
        //     throw new NotEnoughRightsException("The user cannot change roles higher than his own in priority.");
        //
        // command.Roles.Sort();
        // List<ulong> intersectionRoles = community
        //     .GetIntersectionRoles(command.Roles)
        //     .Select(x => x.Id)
        //     .ToList();
        //
        // member.UpdateRoles(intersectionRoles);
        
        return Unit.Value;
    }
}