using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Communities;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Roles.Commands;

public sealed record CreateRoleCommand(
    ulong CommunityId,
    string Name,
    UserPermissions Permissions,
    uint? Priority,
    bool IsMentionable,
    uint Color
):ICommunitiesCommand<RoleViewModel>, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.ManageCommunity | UserPermissions.ManageRole;
}