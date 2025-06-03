using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.Contracts;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Messages.Module.Services.Commands;

public record SendMessageCommand(
  ulong ChannelId,
  string? Text,
  Embed? Embed,
  MessageReferenceViewModel? Reference,
  IAsyncEnumerable<AttachmentFileViewModel> Files
): IMessagingCommand<MessageViewModel>, IRequiredChannelPermissions, IRequiredChannelType
{
  public UserPermissions RequiredPermissions => UserPermissions.ViewChannel | UserPermissions.SendMessage;
  public ChannelType AllowedTypes => ChannelType.TextChannel | ChannelType.ForkChannel;
}