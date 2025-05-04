using System.Security.AccessControl;
using Razdor.Communities.Domain.Roles;

namespace Razdor.Communities.Domain.Channels;

public struct VoiceChannelPermissionOverwrite(ulong rawValue) : IChannelPermissionOverwrite
{
    private ulong _rawValue = rawValue;
    public ulong RawValue => _rawValue;

    public PermissionsState ManageChannel 
    {
        get => this.GetPermission(0);
        set => this.SetPermission(value, 0, ref _rawValue);
    }
    
    public PermissionsState ViewChannel 
    {
        get => this.GetPermission(1);
        set => this.SetPermission(value, 1, ref _rawValue);
    }
    
    public PermissionsState Connect 
    {
        get => this.GetPermission(2);
        set => this.SetPermission(value, 2, ref _rawValue);
    }

    public PermissionsState Speak 
    {
        get => this.GetPermission(3);
        set => this.SetPermission(value, 3, ref _rawValue);
    }

    public PermissionsState MuteMembers 
    {
        get => this.GetPermission(4);
        set => this.SetPermission(value, 4, ref _rawValue);
    }
    
    public PermissionsState DeafenMembers 
    {
        get => this.GetPermission(5);
        set => this.SetPermission(value, 5, ref _rawValue);
    }

    public PermissionsState MoveMembers 
    {
        get => this.GetPermission(6);
        set => this.SetPermission(value, 6, ref _rawValue);
    }
    
    public UserPermissions Apply(UserPermissions permissions)
    {
        PermissionsState state;
        
        state = ManageChannel;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.ManageChannel;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.ManageChannel))
            permissions &= ~UserPermissions.ManageChannel;
        
        state = ViewChannel;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.ViewChannel;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.ViewChannel))
            permissions &= ~UserPermissions.ViewChannel;

        state = Connect;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.Connect;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.Connect))
            permissions &= ~UserPermissions.Connect;
        
        state = Speak;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.Speak;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.Speak))
            permissions &= ~UserPermissions.Speak;

        state = MuteMembers;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.MuteMembers;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.MuteMembers))
            permissions &= ~UserPermissions.MuteMembers;
        
        state = DeafenMembers;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.DeafenMembers;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.DeafenMembers))
            permissions &= ~UserPermissions.DeafenMembers;
        
        state = MoveMembers;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.MoveMembers;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.MoveMembers))
            permissions &= ~UserPermissions.MoveMembers;
        
        return permissions;
    }
}