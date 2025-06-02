namespace Razdor.Messaging.Domain;

public record MessageReference(
    ulong ChannelId,
    ulong MessageId
);