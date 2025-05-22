using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class TextChannel : OverwritesPermissionChannel, IOverwritesOwner
{
    public const UserPermissions AvailablePermissions =
        UserPermissions.ManageChannel
        | UserPermissions.ViewChannel
        | UserPermissions.SendMessage
        | UserPermissions.ManageMessages
        | UserPermissions.AttachFiles
        | UserPermissions.AttachEmbed
        | UserPermissions.UseEmoji
        | UserPermissions.MentionEveryone
        | UserPermissions.ManageFork
        | UserPermissions.CreateFork
        | UserPermissions.SendMessageInFork;
    
    
    /// <summary>
    /// EF constructor
    /// </summary>
    private TextChannel(): this(0, null!, 0, 0, 0, null) { }

    internal TextChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position, 
        ulong parentId, 
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position, ChannelType.TextChannel, parentId, overwrites, AvailablePermissions)
    { }
}