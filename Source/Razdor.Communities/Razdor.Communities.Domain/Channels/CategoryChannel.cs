using System.Collections.ObjectModel;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;

namespace Razdor.Communities.Domain.Channels;

public class CategoryChannel: OverwritesPermissionChannel
{
    public const UserPermissions AvailablePermissions = 
        TextChannel.AvailablePermissions 
        | VoiceChannel.AvailablePermissions;
    
    /// <summary>
    /// EF constructor
    /// </summary>
    private CategoryChannel(): this(0, null!, 0, 0, 0, null) { }
    
    internal CategoryChannel(
        ulong id, 
        string name, 
        ulong communityId, 
        uint position, 
        ulong parentId, 
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position, ChannelType.CategoryChannel, parentId, overwrites, AvailablePermissions)
    { }

    public static CategoryChannel CreateNew(ulong id, ulong communityId, ulong parentId, string name)
        => new CategoryChannel(id, name, communityId, 0, parentId, null);
}