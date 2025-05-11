using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class VoiceChannel: SyncedOverwritesChannel, IChildChannel, ICommunityChannel, IOverwritesOwner
{
    public const UserPermissions AvailablePermissions =
        UserPermissions.ManageChannel
        | UserPermissions.ViewChannel
        | UserPermissions.Connect
        | UserPermissions.Speak
        | UserPermissions.MuteMembers
        | UserPermissions.DeafenMembers
        | UserPermissions.MoveMembers;
    
    internal VoiceChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position, 
        ICommunityChannel? parent, 
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position,ChannelType.Voice, parent, overwrites, AvailablePermissions)
    { }

}