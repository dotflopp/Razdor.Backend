using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Channels.Commands;

public record AddOverwriteCommand(
    ulong ChannelId,
    OverwriteViewModel Overwrite
): ICommunitiesCommand, IRequiredChannelPermissions, IRequiredChannelType
{
    public UserPermissions RequiredPermissions => UserPermissions.ManageChannel;
    public ChannelType AllowedTypes => ChannelType.CategoryChannel | ChannelType.VoiceChannel | ChannelType.TextChannel;
}