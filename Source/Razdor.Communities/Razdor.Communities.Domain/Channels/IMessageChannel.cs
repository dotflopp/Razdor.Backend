using Razdor.Communities.Domain.Channels.Permissions;
using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Domain.Channels;

public interface IMessageChannel : ICommunityChannel
{
    MessageChannelPermissionOverwrite GetPermissionOverwrite(IUser user);
    MessageChannelPermissionOverwrite GetPermissionOverwrite(IRole user);

    MessageChannelPermissionOverwrite SetPermissionOverwrite(IUser user, MessageChannelPermissionOverwrite permission);
    MessageChannelPermissionOverwrite SetPermissionOverwrite(IRole user, MessageChannelPermissionOverwrite permission);
    
    MessageChannelPermissionOverwrite RemovePermissionOverwrite(IUser user);
    MessageChannelPermissionOverwrite RemovePermissionOverwrite(IRole user);
}