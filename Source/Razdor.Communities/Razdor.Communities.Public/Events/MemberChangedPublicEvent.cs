using System.Text.Json.Serialization;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.PublicEvents.Events;

[Flags]
public enum MemberProperties
{
    Nickname = 0x1,
    Description = 0x2,
    Status = 0x4,
    Avatar = 0x8,
    UserProperties = 0xf,
    Roles = 0x100
}

public record MemberChangedPublicEvent(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId,
    MemberProperties Changes,
    CommunicationStatus? Status = null,
    string? Nickname = null,
    string? Avatar = null,
    string? Description = null,
    IReadOnlyCollection<ulong>? Roles = null
) : ICommunityEvent;