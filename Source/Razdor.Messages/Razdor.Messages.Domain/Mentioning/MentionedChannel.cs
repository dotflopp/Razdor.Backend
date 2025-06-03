namespace Razdor.Messages.Domain.Mentioning;

public record MentionedChannel(
    ulong CommunityId,
    ulong ChannelId
);