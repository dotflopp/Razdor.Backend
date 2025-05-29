using System.Text.Json.Serialization;
using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Shared.Module;

namespace Razdor.Identity.Module.Users.Queries;

public sealed record GetUserQuery(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId
) : IIdentityQuery<UserPreviewModel>;