namespace Razdor.Identity.Module.Users.ViewModels;

public record SelfUserViewModel(
    ulong Id, 
    string Email, 
    string IdentityName, 
    string Nickname,
    string? Avatar, 
    DateTimeOffset CredentialsChangeDate
);