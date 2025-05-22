using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Permissions;

public interface IOverwritesPermission
{
    IReadOnlyList<Overwrite> Overwrites { get; }
    
    UserPermissions GetOverwrites(CommunityMember member, UserPermissions inheritedPermissions);
}