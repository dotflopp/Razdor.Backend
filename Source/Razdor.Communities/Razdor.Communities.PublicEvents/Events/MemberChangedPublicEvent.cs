using Razdor.Communities.PublicEvents.ViewModels.Members;

namespace Razdor.Communities.PublicEvents.Events;

[Flags]
public enum MemberProperties
{
    Nickname = 0x1,
    Description = 0x2,
    Status = 0x4,
    Avatar = 0x8,
    All = 0b1111
}

public record MemberChangedPublicEvent(
    ulong CommunityId,
    ulong UserId,
    MemberProperties Changes,
    CommunicationStatus? Status,
    string? Nickname = null,
    string? Avatar = null,
    string? Description = null
) : ICommunityEvent;