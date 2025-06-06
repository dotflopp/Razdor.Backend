using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Messages.Module.Contracts;
using Razdor.Messages.PublicEvents.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Messages.Module.Services.Query;

public record GetMessagesQuery( 
    ulong ChannelId,
    ulong? LastMessageId, 
    int? MessagesCount  
) : IMessagesQuery<IEnumerable<MessageViewModel>>, IRequiredChannelPermissions, IRequiredChannelType
{
    public const int DefaultMessageCount = 100;
    public ChannelType AllowedTypes => ChannelType.TextChannel | ChannelType.ForkChannel;
    public UserPermissions RequiredPermissions => UserPermissions.ViewChannel;
}