﻿using Razdor.Identity.Module.Auth.Commands.ViewModels;
using Razdor.Identity.Module.Contracts;

namespace Razdor.Identity.Module.Auth.Commands;

public sealed record SignupCommand(
    string IdentityName,
    string Email,
    string Password
) : IIdentityCommand<AccessToken>;