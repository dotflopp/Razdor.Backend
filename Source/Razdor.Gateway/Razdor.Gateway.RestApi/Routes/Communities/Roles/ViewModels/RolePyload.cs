using Razdor.Communities.Domain.Permissions;

namespace Razdor.RestApi.Routes.Communities.Roles.ViewModels;

public record RolePyload(
    string Name,
    UserPermissions Permissions,
    bool IsMentionable,
    uint? Priority,
    uint Color
);