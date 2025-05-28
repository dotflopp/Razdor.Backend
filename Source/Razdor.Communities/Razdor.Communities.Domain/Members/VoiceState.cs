namespace Razdor.Communities.Domain.Members;

public record VoiceState(
    ulong ChannelId,
    bool IsDeafened,
    bool IsMuted,
    bool IsSelfDeafened,
    bool IsSelfMuted
)
{
    public static VoiceState Default { get; } = new VoiceState(0, false, false, false, false);
}