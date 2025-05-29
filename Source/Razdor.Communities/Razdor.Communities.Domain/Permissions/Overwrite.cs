namespace Razdor.Communities.Domain.Permissions;

public record Overwrite(
    ulong TargetId,
    PermissionTargetType TargetType,
    OverwritePermissions Permissions
);