using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Members.Commands;

public record AddMemberRoleCommand(
    ulong CommunityId,
    ulong UserId,
    ulong RoleId
): ICommunitiesCommand, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.ManageRole;
}