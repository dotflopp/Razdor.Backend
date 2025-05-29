using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Module;

namespace Razdor.Communities.Services.Services.Communities.ViewModels;

public record RoleViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id,
    uint Priority,
    string Name,
    UserPermissions Permissions,
    bool IsMentioned
)
{
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