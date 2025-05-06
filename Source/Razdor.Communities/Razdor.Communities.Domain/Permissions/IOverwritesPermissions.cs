using Razdor.Communities.Domain.Channels.Permissions;
using Razdor.Communities.Domain.Members;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public interface IOverwritesPermissions
{
    IReadOnlyDictionary<ulong, PermissionOverwrite> RolesPermission { get; }
    IReadOnlyDictionary<ulong, PermissionOverwrite> UsersPermission { get; }
    
    public UserPermissions CalculateUserPermissions(ICommunityUser user);
}