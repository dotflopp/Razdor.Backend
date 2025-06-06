using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Rules;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Communities.Domain.Channels;

public class VoiceChannel : OverwritesPermissionChannel, IOverwritesOwner
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
    ///     EF constructor
    /// </summary>
    private VoiceChannel() : this(0, null!, 0, 0, 0, null) { }

    internal VoiceChannel(
        ulong id,
        string name,
        ulong communityId,
        uint position,
        ulong parentId,
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position, ChannelType.VoiceChannel, parentId, overwrites, AvailablePermissions)
    {
    }

    public static VoiceChannel CreateNew(ulong id, ulong communityId, string name, CommunityChannel? parent = null)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, 0ul);
        ArgumentOutOfRangeException.ThrowIfEqual(communityId, 0ul);
        ArgumentException.ThrowIfNullOrEmpty(nameof(name));

        VoiceChannel newChannel = new (id, name, communityId, 0, parent?.Id ?? 0, null);
        
        parent?.ValidateChild(newChannel);
        
        return newChannel;
    }
    
    public override void ValidateChild(CommunityChannel child)
    {
        throw new BusinesRuleValidationException(
            new ForkAndVoiceСannotHaveDescendants()
        );
    }
}