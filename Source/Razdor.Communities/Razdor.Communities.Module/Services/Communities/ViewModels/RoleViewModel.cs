using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.Module.Services.Communities.ViewModels;

public record RoleViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id,
    uint Priority,
    string Name,
    UserPermissions Permissions,
    bool IsMentionable,
    ulong Color
)
{
    public static RoleViewModel From(Role role)
    {
        return new RoleViewModel(
            role.Id,
            role.Priority,
            role.Name,
            role.Permissions,
            role.IsMentionable,
            role.Color
        );
    }
}