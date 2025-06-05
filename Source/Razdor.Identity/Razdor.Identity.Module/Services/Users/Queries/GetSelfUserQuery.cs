using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Services.Users.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Identity.Module.Services.Users.Queries;

public sealed record GetSelfUserQuery : IIdentityQuery<SelfUserViewModel>, IAuthorizationRequiredMessage;