using Razdor.Communities.Domain.Permissions;

namespace Razdor.RestApi.Routes.Channels.Overwrites.ViewModels;

public record OverwritePyload(    
    PermissionTargetType TargetType,
    OverwritePermissions Permissions
);