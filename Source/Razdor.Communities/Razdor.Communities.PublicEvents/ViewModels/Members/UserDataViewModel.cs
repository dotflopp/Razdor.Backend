namespace Razdor.Communities.PublicEvents.ViewModels.Members;

public record UserDataViewModel(
    string IdentityName,
    string Nickname,
    string? Avatar,
    CommunicationStatus CommunicationStatus
);