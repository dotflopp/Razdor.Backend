using System.Text.Json.Serialization;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Services.Users.ViewModels;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Identity.Module.Services.Users.Queries;

public sealed record GetUserQuery(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId
) : IIdentityQuery<UserPreviewModel>;