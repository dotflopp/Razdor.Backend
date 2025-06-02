namespace Razdor.Messaging.Domain.Mentioning;

public record MentionedChannel(
    ulong CommunityId,
    ulong ChannelId
);