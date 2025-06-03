namespace Razdor.Messages.Domain;

public record MessageReference(
    ulong ChannelId,
    ulong MessageId
);