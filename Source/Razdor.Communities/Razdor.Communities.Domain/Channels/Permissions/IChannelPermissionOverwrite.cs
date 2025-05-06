using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Channels.Permissions;

public interface IChannelPermissionOverwrite
{
    UserPermissions RejectPermissions(UserPermissions permissions);
    UserPermissions GrantPermissions(UserPermissions permissions);
}