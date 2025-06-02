using Razdor.Messaging.Domain.Mentioning;
using Razdor.Shared.Domain;

namespace Razdor.Messaging.Domain.Events;

public record MessageSentEvent(
    ulong Id, 
    ulong UserId,
    ulong ChannelId,
    string? Text,
    DateTimeOffset СreatedAt,
    MessageReference? MessageReference,
    Embed? Embed,
    IReadOnlyCollection<Attachment> Attachments,
    Mentions? Mentions
) : IDomainEvent
{
    public static MessageSentEvent From(Message message)
        => new MessageSentEvent(
            message.Id,
            message.UserId,
            message.ChannelId,
            message.Text,
            message.CreatedAt,
            message.Reference,
            message.Embed,
            message.Attachments,
            message.Mentions
        );
}