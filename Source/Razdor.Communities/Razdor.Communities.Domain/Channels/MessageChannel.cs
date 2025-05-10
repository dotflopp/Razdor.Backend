using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class MessageChannel : SyncedOverwritesChannel, IChildChannel, ICommunityChannel, IOverwritesOwner
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
    
    public MessageChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position, 
        ICommunityChannel? parent, 
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position, ChannelType.Message, parent, overwrites, AvailablePermissions)
    {
    }
}