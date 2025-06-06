using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Rules;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Communities.Domain.Channels;

public class CategoryChannel : OverwritesPermissionChannel
{
    public const UserPermissions AvailablePermissions =
        TextChannel.AvailablePermissions
        | VoiceChannel.AvailablePermissions;

    /// <summary>
    ///     EF constructor
    /// </summary>
    private CategoryChannel() : this(0, null!, 0, 0, 0, null) { }

    internal CategoryChannel(
        ulong id,
        string name,
        ulong communityId,
        uint position,
        ulong parentId,
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position, ChannelType.CategoryChannel, parentId, overwrites, AvailablePermissions)
    {
    }

    public static CategoryChannel CreateNew(ulong id, ulong communityId, string name, CommunityChannel? parent = null)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, 0ul);
        ArgumentOutOfRangeException.ThrowIfEqual(communityId, 0ul);
        ArgumentException.ThrowIfNullOrEmpty(nameof(name));
        
        CategoryChannel newChannel = new(id, name, communityId, 0, parent?.Id ?? 0, null);
        parent?.ValidateChild(newChannel);
        
        return newChannel;
    }
    
    public override void ValidateChild(CommunityChannel child)
    {
        RuleValidationHelper.ThrowIfBroken(
            new CategoryCannotHaveForklOrCategory(child)    
        );
    }
}
