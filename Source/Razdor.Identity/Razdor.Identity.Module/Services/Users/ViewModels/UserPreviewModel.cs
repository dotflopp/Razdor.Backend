using System.Text.Json.Serialization;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Identity.Module.Services.Users.ViewModels;

public sealed record UserPreviewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id,
    string IdentityName,
    string Nickname,
    string? Avatar,
    DisplayedCommunicationStatus Status,
    string? Description
);