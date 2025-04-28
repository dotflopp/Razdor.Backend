using Mediator;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Shared.Features;

namespace Razdor.Identity.Module.Users.Queries;

public record GetUserQuery(
    ulong UserId,
    IServiceIdentity Identity
) : IQuery<UserPreviewViewModel>;