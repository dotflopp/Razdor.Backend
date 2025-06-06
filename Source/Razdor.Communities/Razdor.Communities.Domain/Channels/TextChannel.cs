using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Rules;
using Razdor.Shared.Domain.Rules;

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
    ///     EF constructor
    /// </summary>
    private TextChannel() : this(0, null!, 0, 0, 0, null) { }

    internal TextChannel(
        ulong id,
        string name,
        ulong communityId,
        uint position,
        ulong parentId,
        List<Overwrite>? overwrites
    ) : base(id, name, communityId, position, ChannelType.TextChannel, parentId, overwrites, AvailablePermissions)
    {
    }

    public static TextChannel CreateNew(ulong id, ulong communityId, string name, CommunityChannel? parent = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(name));
         
        TextChannel newChannel = new TextChannel(id, name, communityId, 0, parent?.Id ?? 0, null);
        
        parent?.ValidateChild(newChannel);
        
        return newChannel;
    }
    
    public override void ValidateChild(CommunityChannel child)
    {
        RuleValidationHelper.ThrowIfBroken(
            new TextDescendantCanBeOnlyFork(child)    
        );
    }
}