namespace Razdor.Communities.Module.Services.Members.ViewModels;

public record UserDataViewModel(
    string IdentityName,
    string Nickname,
    string? Avatar,
    CommunicationStatus CommunicationStatus
);