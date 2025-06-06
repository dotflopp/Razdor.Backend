using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Events;

[Flags]
public enum UserProperties
{
    Nickname = 0x1,
    Description = 0x2,
    DisplayedStatus = 0x4,
    Avatar = 0x8,
    CredentialsChangeDate = 0x10,
    IsOnline = 0x20,
    SelectedStatus = 0x40,
}

public record UserChangedEvent(ulong UserId) : IDomainEvent
{
    public ulong UserId { get; init; } = UserId;
    public UserProperties UserProperties { get; set; }
    public string? Nickname { get; set; } = null;
    public MediaFileMeta? Avatar { get; set; } = null;
    public DateTimeOffset? CredentialsChangeDate { get; set; } = null;
    public bool? IsOnline { get; set; } = null;
    public DisplayedCommunicationStatus? DisplayedStatus { get; set; } = null;
    public SelectedCommunicationStatus? SelectedStatus { get; set; } = null;
    public string? Description { get; set; } = null;
}