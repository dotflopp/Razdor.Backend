﻿using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Identity.Module.Users.Queries;

public sealed record GetSelfUserQuery : IIdentityQuery<SelfUserViewModel>, IAuthorizationRequiredMessage;