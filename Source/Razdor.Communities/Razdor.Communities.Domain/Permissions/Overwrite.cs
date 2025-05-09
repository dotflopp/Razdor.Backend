namespace Razdor.Communities.Domain.Permissions;

public record Overwrite(
    ulong TargetId,
    PermissionTarget TargetType,
    OverwritePermissions Permissions
);