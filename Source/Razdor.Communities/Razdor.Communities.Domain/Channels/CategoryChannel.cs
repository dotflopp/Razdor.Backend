using System.Collections.ObjectModel;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class CategoryChannel: CommunityChannel, ICommunityChannel, IOverwritesOwner, IOverwritesPermission
{
    private List<Overwrite>? _overwrites;
    
    public const UserPermissions AvailablePermissions = 
        MessageChannel.AvailablePermissions 
        | VoiceChannel.AvailablePermissions;
    
    internal CategoryChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position, 
        List<Overwrite> overwrites
    ) : base(id, name, communityId, position, ChannelType.Category)
    {
        _overwrites = overwrites;
    }
    
    public override IReadOnlyList<Overwrite> Overwrites => 
        _overwrites?.AsReadOnly() ?? ReadOnlyCollection<Overwrite>.Empty;

    public void SetRolePermission(ulong roleId, OverwritePermissions permission)
    {
        if (_overwrites == null)
            _overwrites = new List<Overwrite>();
        
        permission = new(
            permission.Allow & AvailablePermissions,
            permission.Deny & AvailablePermissions
        );
        
        ChannelHelper.SetRolePermissions(_overwrites, roleId, permission);
    }
    
    public void SetUserPermission(ulong userId, OverwritePermissions permission)
    {
        if (_overwrites == null)
            _overwrites = new List<Overwrite>();
        
        permission = new(
            permission.Allow & AvailablePermissions,
            permission.Deny & AvailablePermissions
        );

        ChannelHelper.SetUserPermissions(_overwrites, userId, permission); ;
    }

    public void RemoveUserPermission(ulong userId)
    {
        if (_overwrites == null)
            return;
        
        ChannelHelper.RemoveEntityPermissions(_overwrites, userId);
    }

    public void RemoveRolePermission(ulong roleId)
    {
        if (_overwrites == null)
            return;
        
        ChannelHelper.RemoveEntityPermissions(_overwrites, roleId);
    }
}