using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Messages.Module.Contracts;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Messages.Module.Services.Query;

public record GetMessagesQuery( 
    ulong ChannelId,
    ulong? LastMessageId, 
    int? MessagesCount  
) : IMessagingQuery<IEnumerable<MessageViewModel>>, IRequiredChannelPermissions, IRequiredChannelType
{
    public const int DefaultMessageCount = 100;
    public ChannelType AllowedTypes => ChannelType.TextChannel | ChannelType.ForkChannel;
    public UserPermissions RequiredPermissions => UserPermissions.ViewChannel;
}