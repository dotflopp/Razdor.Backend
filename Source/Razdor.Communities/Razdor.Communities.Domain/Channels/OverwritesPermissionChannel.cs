using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Channels.Events;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Extensions;

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
    public override sealed bool IsSyncing => ParentId != 0 && _overwrites == null;

    public override sealed IReadOnlyList<Overwrite> Overwrites
        => _overwrites?.AsReadOnly() ?? ReadOnlyCollection<Overwrite>.Empty;

    public override sealed void RemoveParent(List<Overwrite> inheritedOverwrites)
    {
        AddDomainEvent(new ParentChannelRemoved(Id, ParentId));
        
        ParentId = 0;
        _overwrites = inheritedOverwrites;
    }

    public void SetOverwrite(
        ulong entityId,
        OverwritePermissions permission,
        PermissionTargetType targetType,
        List<Overwrite>? inherited
    )
    {
        InitOverwritesIfNull(ref _overwrites, inherited);
        RemoveOverwrite(entityId, inherited);
        
        permission = new OverwritePermissions(
            permission.Allow & _availablePermissions,
            permission.Deny & _availablePermissions
        );

        Overwrite overwrite = new(entityId, targetType, permission);
        int index = _overwrites.FindIndex(x => x.TargetId < entityId);
        _overwrites.Insert(index + 1, overwrite);
    }

    public Overwrite? RemoveOverwrite(ulong entityId, List<Overwrite>? inherited)
    {
        List<Overwrite>? overwrites = _overwrites;

        InitOverwritesIfNull(ref overwrites, inherited);

        int targetIndex = overwrites.BinarySearchBy(entityId, x => x.TargetId);

        if (targetIndex < 0)
            return null;

        Overwrite removed = overwrites[targetIndex];
        overwrites.RemoveAt(targetIndex);
        _overwrites = overwrites;
        
        return removed;
    }

    private void InitOverwritesIfNull([NotNull] ref List<Overwrite>? overwrites, List<Overwrite>? inherited)
    {
        if (!IsSyncing)
            overwrites ??= new List<Overwrite>();
        else if (inherited != null)
            overwrites = inherited;
        else ThrowInheritedNullException();
    }

    [DoesNotReturn]
    private static void ThrowInheritedNullException()
    {
        throw new ArgumentNullException("The synchronized channel must have inheritable Overwrites.");
    }
}