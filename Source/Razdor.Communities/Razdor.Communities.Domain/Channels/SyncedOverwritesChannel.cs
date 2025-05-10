using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public abstract class SyncedOverwritesChannel : CommunityChannel, IChildChannel, ICommunityChannel, IOverwritesOwner, IOverwritesPermission
{
    private List<Overwrite>? _overwrites;
    
    internal SyncedOverwritesChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position,
        ChannelType type,
        ICommunityChannel? parent,
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position, type)
    {
        Parent = parent;
        _overwrites = overwrites;
    }
    
    public ICommunityChannel? Parent { get; }
    
    [MemberNotNullWhen(true, nameof(Parent), nameof(_overwrites))]
    public bool IsSyncing => _overwrites == null && Parent != null;
    
    public override IReadOnlyList<Overwrite> Overwrites => IsSyncing 
        ? Parent.Overwrites 
        : _overwrites?.AsReadOnly() ?? ReadOnlyCollection<Overwrite>.Empty;
    

    public void SetRolePermission(ulong roleId, OverwritePermissions permission)
    {
        InitOverwritesIfNull();
        ChannelHelper.SetRolePermissions(_overwrites, roleId, permission);
    }

    public void SetUserPermission(ulong userId, OverwritePermissions permission)
    {
        InitOverwritesIfNull();
        ChannelHelper.SetUserPermissions(_overwrites, userId, permission);
    }

    public void RemoveUserPermission(ulong userId)
        => RemoveEntityPermissions(userId);

    public void RemoveRolePermission(ulong roleId)
        => RemoveEntityPermissions(roleId);

    private void RemoveEntityPermissions(ulong entityId)
    {
        List<Overwrite>? overwrites = _overwrites; 
        
        if (IsSyncing)
            overwrites = Parent.Overwrites.ToList();
        
        if (overwrites == null)
            return;
        
        ChannelHelper.RemoveEntityPermissions(overwrites, entityId);
    }
    
    [MemberNotNull(nameof(_overwrites))]
    private void InitOverwritesIfNull()
    {
        _overwrites ??= (Parent != null)
            ? Parent.Overwrites.ToList() 
            : new List<Overwrite>();
    }
}