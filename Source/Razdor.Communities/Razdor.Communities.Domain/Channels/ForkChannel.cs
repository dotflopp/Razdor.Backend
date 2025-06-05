using System.Collections.ObjectModel;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Rules;
using Razdor.Shared.Domain;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Communities.Domain.Channels;

public class ForkChannel : CommunityChannel, IEntity<ulong>
{
    /// <summary>
    ///     EF constructor
    /// </summary>
    private ForkChannel() : this(0, null!, 0, 0, 0) { }

    internal ForkChannel(
        ulong id,
        string name,
        ulong communityId,
        uint position,
        ulong parentId
    ) : base(id, name, communityId, parentId, position, ChannelType.ForkChannel)
    {
    }
    public override bool IsSyncing => true;
    public override IReadOnlyList<Overwrite> Overwrites => ReadOnlyCollection<Overwrite>.Empty;

    public override sealed void RemoveParent(List<Overwrite> inheritedOverwrites)
    {
        throw new BusinesRuleValidationException(
            new ForkChannelCannotExistWithoutParent()    
        );
    }
    public override UserPermissions GetPermissionsWithOverwrites(CommunityMember member, UserPermissions inheritedPermissions)
    {
        UserPermissions result = base.GetPermissionsWithOverwrites(member, inheritedPermissions);

        if (result.HasFlag(UserPermissions.SendMessageInFork))
            return result | UserPermissions.SendMessage;

        return result;
    }

    public static ForkChannel CreateNew(ulong id, ulong communityId, ulong parentId, string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(name));
        return new ForkChannel(id, name, communityId, 0, parentId);
    }
}