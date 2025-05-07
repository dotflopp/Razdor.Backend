using Razdor.Identity.Domain.Users;

namespace Razdor.Identity.Module.Users.ViewModels;

public record UserPreviewModel(
    ulong Id,
    string IdentityName, 
    string Nickname,
    string? Avatar,
    UserCommunicationStatus Status,
    string Description
);