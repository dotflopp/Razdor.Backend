using System.Text.Json.Serialization;
using Razdor.Api.Routes.Messaging.ViewModels;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.Services.Commands;
using Razdor.Messages.Module.Services.Commands.ViewModels;

namespace Razdor.Api.Serialization;

[JsonSerializable(typeof(MessagePyload))]
[JsonSerializable(typeof(Embed))]
[JsonSerializable(typeof(EmbedField))]
[JsonSerializable(typeof(EmbedFooter))]
[JsonSerializable(typeof(AttachmentViewModel))]
[JsonSerializable(typeof(MentionedChannelViewModel))]
[JsonSerializable(typeof(MentionedRoleViewModel))]
[JsonSerializable(typeof(MentionedUserViewModel))]
[JsonSerializable(typeof(MessageReferenceViewModel))]
[JsonSerializable(typeof(MessageViewModel))]
[JsonSerializable(typeof(SendMessageCommand))]
public partial class MessagesJsonSerializationContext: JsonSerializerContext
{
    
}