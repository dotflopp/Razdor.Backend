namespace Razdor.Communities.Module.Services.Members.ViewModels;

public record MemberProfileViewModel(
    string IdentityName,
    string Nickname,
    string? Avatar
);