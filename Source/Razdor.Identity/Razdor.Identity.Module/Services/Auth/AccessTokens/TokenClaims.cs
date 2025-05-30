using System.Text.Json.Serialization;
using Razdor.Shared.Module;

namespace Razdor.Identity.Module.Auth.AccessTokens;

public record TokenClaims(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId,
    DateTimeOffset CreationTime
);