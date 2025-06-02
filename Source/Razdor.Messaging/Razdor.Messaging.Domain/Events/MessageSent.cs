using Razdor.Messaging.Domain.Mentioning;

namespace Razdor.Messaging.Domain.Events;

public record MessageSent(
    ulong Id, 
    ulong CommunityId,
    ulong UserId,
    ulong ChannelId,
    string? Text,
    DateTimeOffset СreatedAt,
    MessageReference? MessageReference = null,
    Embed? Embed = null,
    bool IsMentionEveryone = false,
    bool IsPinned = false,
    List<Attachment>? Attachments = null,
    Mentions? Mentions = null
);