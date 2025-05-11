using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;

namespace Razdor.Communities.Services.Communities.ViewModels;

public record RoleViewModel(
    ulong Id,
    uint Priority,
    string Name,
    UserPermissions Permissions,
    bool IsMentioned
){
    public static RoleViewModel From(Role role)
    {
        return new RoleViewModel(
            role.Id,
            role.Priority,
            role.Name,
            role.Permissions,
            role.IsMentioned
        );
    }
}