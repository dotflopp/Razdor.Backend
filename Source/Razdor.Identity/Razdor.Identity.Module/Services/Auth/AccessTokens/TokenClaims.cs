using System.Text.Json.Serialization;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Identity.Module.Services.Auth.AccessTokens;

public record TokenClaims(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId,
    DateTimeOffset CreationTime
);