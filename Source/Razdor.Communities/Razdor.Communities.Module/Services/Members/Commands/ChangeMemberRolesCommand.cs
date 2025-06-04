using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Members.Commands;

public record ChangeMemberRolesCommand(
    ulong CommunityId,
    ulong UserId,
    List<ulong> Roles
): ICommunitiesCommand, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.ManageRole;
}