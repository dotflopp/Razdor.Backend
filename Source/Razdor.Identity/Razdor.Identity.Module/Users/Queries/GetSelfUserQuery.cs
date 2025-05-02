using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Users.ViewModels;

namespace Razdor.Identity.Module.Users.Queries;

public record GetSelfUserQuery : IIdentityQuery<SelfUserViewModel>;