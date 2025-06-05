using Razdor.Communities.Domain.Permissions;

namespace Razdor.Api.Routes.Communities.Roles.ViewModels;

public record RolePyload(
    string Name,
    UserPermissions Permissions,
    bool IsMentionable,
    uint? Priority,
    uint Color
);