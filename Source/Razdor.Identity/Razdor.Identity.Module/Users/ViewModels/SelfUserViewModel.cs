using Razdor.Identity.Domain.Users;

namespace Razdor.Identity.Module.Users.ViewModels;

public record SelfUserViewModel(
    ulong Id, 
    string Email, 
    string IdentityName, 
    string Nickname,
    string? Avatar, 
    DateTimeOffset CredentialsChangeDate,
    SelectedCommunicationStatus SelectedStatus,
    DisplayedCommunicationStatus Status,
    string? Description
);