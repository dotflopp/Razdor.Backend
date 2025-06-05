using System.Text.Json.Serialization;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Identity.Module.Services.Users.ViewModels;

public sealed record SelfUserViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
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