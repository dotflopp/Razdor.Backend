using Razdor.Identity.Domain.Users;

namespace Razdor.Identity.Module.Users.ViewModels;

public record SelfUserViewModel(
    ulong Id, 
    string Email, 
    string IdentityName, 
    string Nickname,
    string? Avatar, 
    DateTimeOffset CredentialsChangeDate,
    UserCommunicationStatus Status,
    string? Description
);