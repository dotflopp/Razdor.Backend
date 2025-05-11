using System.Collections.ObjectModel;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class CategoryChannel: SyncedOverwritesChannel, ICommunityChannel, IOverwritesOwner, IOverwritesPermission
{
    public const UserPermissions AvailablePermissions = 
        MessageChannel.AvailablePermissions 
        | VoiceChannel.AvailablePermissions;
    
    internal CategoryChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position, 
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position,ChannelType.Voice, null, overwrites, AvailablePermissions)
    { }
}