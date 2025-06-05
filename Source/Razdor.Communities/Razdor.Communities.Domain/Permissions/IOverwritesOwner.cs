namespace Razdor.Communities.Domain.Permissions;

public interface IOverwritesOwner : IOverwritesPermission
{
    public void SetOverwrite(
        ulong entityId,
        OverwritePermissions permission,
        PermissionTargetType targetType,
        List<Overwrite>? inherited
    );

    Overwrite? RemoveOverwrite(ulong entityId, List<Overwrite>? inherited);

}