using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.PublicEvents.ViewModels.Communities;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Communities.Module.Services.Roles.Commands;

public class CreateRoleCommandHandler(
    ICommunitiesRepository communities,
    ICommunityPermissionsAccessor permissionsAccessor,
    IRequestSenderContext sender,
    SnowflakeGenerator snowflake
) : ICommandHandler<CreateRoleCommand, RoleViewModel>
{

    public async ValueTask<RoleViewModel> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        (UserPermissions permissions, uint userPriority) = await permissionsAccessor.GetMemberPermissionsAndPriorityAsync(
            command.CommunityId, sender.User.Id, cancellationToken
        );


        if (!permissions.HasFlag(UserPermissions.Administrator) && ((command.Permissions & permissions) != command.Permissions))
            throw new NotEnoughRightsException("A user cannot grant rights that they do not have.");

        Community? community = await communities.FindAsync(command.CommunityId, cancellationToken)
            ?? throw new InvalidOperationException("Authorized access to a non-existent community");

        uint rolePriority = command.Priority ?? 0u;
        if (rolePriority == 0)
             rolePriority = (uint)(community.Roles.Count) + 1;

        if (command.Priority <= userPriority)
            throw new NotEnoughRightsException("A user cannot create a role with a higher or equal priority.");
        

        Role role = Role.CreateNew(
            snowflake.Next(),
            command.Name,
            command.CommunityId,
            command.Permissions,
            command.IsMentionable,
            rolePriority,
            command.Color
        );
        
        community.AddRole(
            role
        );

        await communities.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return RoleViewModel.From(role);
    }
}