using Razdor.Communities.Domain.Permissions;

namespace Razdor.Api.Routes.Channels.Overwrites.ViewModels;

public record OverwritePyload(    
    PermissionTargetType TargetType,
    OverwritePermissions Permissions
);