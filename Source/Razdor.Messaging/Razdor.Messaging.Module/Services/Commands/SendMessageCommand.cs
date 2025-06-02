using System.Text.Json.Serialization;
using Mediator;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Messaging.Domain;
using Razdor.Messaging.Module.Contracts;
using Razdor.Messaging.Module.Services.Commands.ViewModels;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Messaging.Module.Services.Commands;

public record SendMessageCommand(
  ulong ChannelId,
  string? Text,
  Embed? Embed,
  MessageReferenceViewModel? Reference
): IMessagingCommand<MessageViewModel>, IRequiredChannelPermissions, IRequiredChannelType
{
  public UserPermissions RequiredPermissions => UserPermissions.ViewChannel | UserPermissions.SendMessage;
  public ChannelType AllowedTypes => ChannelType.TextChannel | ChannelType.ForkChannel;
}