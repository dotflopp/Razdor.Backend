namespace Razdor.Communities.Domain.Members;

public record VoiceState(
    ulong? ChannelId,
    bool IsDeafened,
    bool IsMuted,
    bool IsSelfDeafened,
    bool IsSelfMuted
);