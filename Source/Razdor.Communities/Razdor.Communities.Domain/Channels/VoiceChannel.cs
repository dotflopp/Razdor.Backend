using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class VoiceChannel: OverwritesPermissionChannel, IOverwritesOwner
{
    public const UserPermissions AvailablePermissions =
        UserPermissions.ManageChannel
        | UserPermissions.ViewChannel
        | UserPermissions.Connect
        | UserPermissions.Speak
        | UserPermissions.MuteMembers
        | UserPermissions.DeafenMembers
        | UserPermissions.MoveMembers;
    
    
    /// <summary>
    /// EF constructor
    /// </summary>
    private VoiceChannel(): this(0, null!, 0, 0, 0, null) { }

    internal VoiceChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position, 
        ulong parentId, 
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position, ChannelType.VoiceChannel, parentId, overwrites, AvailablePermissions)
    { }

    public static VoiceChannel CreateNew(ulong id, ulong communityId, ulong parentId, string name)
        => new (id, name, communityId, 0, parentId, null);
}