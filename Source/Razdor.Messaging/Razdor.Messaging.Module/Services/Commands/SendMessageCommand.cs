using Mediator;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Messaging.Domain;
using Razdor.Messaging.Module.Contracts;
using Razdor.Messaging.Module.Services.Commands.ViewModels;

namespace Razdor.Messaging.Module.Services.Commands;

public record SendMessageCommand(
  ulong ChannelId,
  string? Text,
  Embed? Embed,
  MessageReferenceViewModel? Reference
): IMessagingCommand<MessageViewModel>, IRequiredChannelPermissions
{
  public UserPermissions RequiredPermissions => UserPermissions.ViewChannel | UserPermissions.SendMessage;
}