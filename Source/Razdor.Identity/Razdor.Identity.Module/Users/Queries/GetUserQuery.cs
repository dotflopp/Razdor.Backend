using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Shared.Module.Identities;

namespace Razdor.Identity.Module.Users.Queries;

public record GetUserQuery(
    ulong UserId
) : IIdentityQuery<UserPreviewModel>;