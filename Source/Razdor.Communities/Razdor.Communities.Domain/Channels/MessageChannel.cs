using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class MessageChannel : CommunityChannel, IChildChannel, IOverwritesOwner
{
    private List<Overwrite>? _overwrites;
    
    public MessageChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position,
        List<Overwrite> overwrites,
        ICommunityChannel? parent
    ) : base(id, name, communityId, position)
    {
        _overwrites = overwrites;
        Parent = parent;
    }

    public override IReadOnlyList<Overwrite> Overwrites 
        => IsSyncing ? Parent.Overwrites : _overwrites?.AsReadOnly() ?? ReadOnlyCollection<Overwrite>.Empty;
    public ICommunityChannel? Parent { get; }
    
    [MemberNotNullWhen(true, nameof(Parent), nameof(_overwrites))]
    public bool IsSyncing => _overwrites == null && Parent != null;

    [MemberNotNull(nameof(_overwrites))]
    private void InitOverwritesIfNull()
    {
        _overwrites ??= (Parent != null)
            ? Parent.Overwrites.ToList() 
            : new List<Overwrite>();
    }

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
        if (IsSyncing)
        {
            List<Overwrite> overwrites = Parent.Overwrites.ToList();
            ChannelHelper.RemoveEntityPermissions(_overwrites, entityId);

            if (overwrites.Count == Parent.Overwrites.Count)
                return;
            
            _overwrites = overwrites;
            return;
        }
        
        if (_overwrites == null)
            return;
        
        ChannelHelper.RemoveEntityPermissions(_overwrites, entityId);  
    }
}