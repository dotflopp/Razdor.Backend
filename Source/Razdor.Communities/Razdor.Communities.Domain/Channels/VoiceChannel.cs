using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class VoiceChannel: SyncedOverwritesChannel, IChildChannel, ICommunityChannel, IOverwritesOwner
{
    internal VoiceChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position, 
        ICommunityChannel? parent, 
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position,ChannelType.Voice, parent, overwrites)
    { }

}