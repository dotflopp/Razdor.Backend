using Mediator;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Shared.Features;

namespace Razdor.Identity.Module.Users.Queries;

public record GetMeQuery(
    IServiceIdentity Identity
) : IQuery<AccountViewModel>;