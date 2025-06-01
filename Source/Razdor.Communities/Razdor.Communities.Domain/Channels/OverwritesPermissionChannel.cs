using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public abstract class OverwritesPermissionChannel : CommunityChannel, IOverwritesOwner
{
    private readonly UserPermissions _availablePermissions;
    private List<Overwrite>? _overwrites;

    /// <summary>
    /// EF constructor
    /// </summary>
    private OverwritesPermissionChannel() : this(0, null!, 0, 0, (ChannelType)0, 0, null, UserPermissions.None)
    {
    }
    
    internal OverwritesPermissionChannel(
        ulong id,
        string name,
        ulong communityId,
        uint position,
        ChannelType type,
        ulong parentId,
        List<Overwrite>? overwrites,
        UserPermissions availablePermissions
    ) : base(id, name, communityId, parentId, position, type)
    {
        _overwrites = overwrites;
        _availablePermissions = availablePermissions;
    }

    [MemberNotNullWhen(true, nameof(_overwrites))]
    public override bool IsSyncing => ParentId != 0 && _overwrites == null;

    public override IReadOnlyList<Overwrite> Overwrites
        => _overwrites?.AsReadOnly() ?? ReadOnlyCollection<Overwrite>.Empty;

    public void SetRolePermission(ulong roleId, OverwritePermissions permission, List<Overwrite>? inherited)
    {
        SetEntityPermissions(roleId, permission, PermissionTargetType.Role, inherited);
    }

    public void SetUserPermission(ulong userId, OverwritePermissions permission, List<Overwrite>? inherited)
    {
        SetEntityPermissions(userId, permission, PermissionTargetType.User, inherited);
    }

    public void RemoveUserPermission(ulong userId, List<Overwrite>? inherited)
    {
        RemoveEntityPermissions(userId, inherited);
    }

    public void RemoveRolePermission(ulong roleId, List<Overwrite>? inherited)
    {
        RemoveEntityPermissions(roleId, inherited);
    }

    private void SetEntityPermissions(
        ulong entityId,
        OverwritePermissions permission,
        PermissionTargetType targetTypeType,
        List<Overwrite>? inherited
    )
    {
        InitOverwritesIfNull(inherited);
        RemoveEntityPermissions(entityId, inherited);

        permission = new OverwritePermissions(
            permission.Allow & _availablePermissions,
            permission.Deny & _availablePermissions
        );

        Overwrite overwrite = new(entityId, targetTypeType, permission);
        int index = _overwrites.FindIndex(x => x.TargetId < entityId);
        _overwrites.Insert(index + 1, overwrite);
    }

    private void RemoveEntityPermissions(ulong entityId, List<Overwrite>? inherited)
    {
        List<Overwrite>? overwrites = _overwrites;

        if (!IsSyncing)
            overwrites ??= new List<Overwrite>();
        else if (inherited != null)
            overwrites = inherited;
        else ThrowInheritedNullException();

        int targetIndex = overwrites.FindIndex(x => x.TargetId == entityId);

        if (targetIndex < 0)
            return;

        overwrites.RemoveAt(targetIndex);
        _overwrites = overwrites;
    }

    [MemberNotNull(nameof(_overwrites))]
    private void InitOverwritesIfNull(List<Overwrite>? inherited)
    {
        if (!IsSyncing)
            _overwrites ??= new List<Overwrite>();
        else if (inherited != null)
            _overwrites = inherited;
        else ThrowInheritedNullException();
    }

    [DoesNotReturn]
    private static void ThrowInheritedNullException()
    {
        throw new ArgumentNullException("The synchronized channel must have inheritable Overwrites.");
    }
}