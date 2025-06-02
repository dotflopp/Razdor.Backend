namespace Razdor.Messaging.Domain;

public record MessageReference(
    ulong CommunityId,
    ulong ChannelId,
    ulong MessageId
);