using System.Runtime.InteropServices.Swift;
using Razdor.Communities.Domain.Roles;

namespace Razdor.Communities.Domain.Channels;

public struct MessageChannelPermissionOverwrite(ulong rawValue) : IChannelPermissionOverwrite
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
    
    public PermissionsState SendMessage 
    {
        get => this.GetPermission(2);
        set => this.SetPermission(value, 2, ref _rawValue);
    }
    
    public PermissionsState ManageMessages 
    {
        get => this.GetPermission(3);
        set => this.SetPermission(value, 3, ref _rawValue);
    }
    
    public PermissionsState AttachFiles 
    {
        get => this.GetPermission(4);
        set => this.SetPermission(value, 4, ref _rawValue);
    }
    
    public PermissionsState AttachEmbed 
    {
        get => this.GetPermission(5);
        set => this.SetPermission(value, 5, ref _rawValue);
    }
    
    public PermissionsState UseEmoji 
    {
        get => this.GetPermission(6);
        set => this.SetPermission(value, 6, ref _rawValue);
    }
    
    public PermissionsState MentionEveryone 
    {
        get => this.GetPermission(7);
        set => this.SetPermission(value, 7, ref _rawValue);
    }
    
    public PermissionsState ManageFork 
    {
        get => this.GetPermission(8);
        set => this.SetPermission(value, 8, ref _rawValue);
    }
    
    public PermissionsState CreateFork 
    {
        get => this.GetPermission(9);
        set => this.SetPermission(value, 9, ref _rawValue);
    }
    
    public PermissionsState SendMessagesInFork 
    {
        get => this.GetPermission(10);
        set => this.SetPermission(value, 10, ref _rawValue);
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

        state = SendMessage;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.SendMessage;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.SendMessage))
            permissions &= ~UserPermissions.SendMessage;
        
        state = ManageMessages;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.ManageMessages;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.ManageMessages))
            permissions &= ~UserPermissions.ManageMessages;
        
        state = AttachFiles;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.AttachFiles;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.AttachFiles))
            permissions &= ~UserPermissions.AttachFiles;
        
        state = AttachEmbed;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.AttachFiles;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.AttachEmbed))
            permissions &= ~UserPermissions.AttachEmbed;
        
        state = UseEmoji;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.UseEmoji;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.UseEmoji))
            permissions &= ~UserPermissions.UseEmoji;
        
        state = MentionEveryone;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.MentionEveryone;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.MentionEveryone))
            permissions &= ~UserPermissions.MentionEveryone;
        
        state = ManageFork;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.ManageFork;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.ManageFork))
            permissions &= ~UserPermissions.ManageFork;
        
        state = CreateFork;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.CreateFork;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.CreateFork))
            permissions &= ~UserPermissions.CreateFork;
        
        state = SendMessagesInFork;
        if (state == PermissionsState.Granted)
            permissions |= UserPermissions.SendMessagesInFork;
        else if (state == PermissionsState.Rejected && permissions.HasFlag(UserPermissions.SendMessagesInFork))
            permissions &= ~UserPermissions.SendMessagesInFork;
        
        return permissions;
    }
}
