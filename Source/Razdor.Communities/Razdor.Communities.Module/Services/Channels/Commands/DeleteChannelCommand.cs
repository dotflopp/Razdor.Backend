using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public record DeleteChannelCommand(
    ulong ChannelId
) : ICommunitiesCommand, IRequiredChannelPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.ManageChannel;
}