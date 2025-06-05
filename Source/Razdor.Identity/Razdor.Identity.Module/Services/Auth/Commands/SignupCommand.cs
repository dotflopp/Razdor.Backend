using Razdor.Identity.Module.Contracts;
using Razdor.Identity.Module.Services.Auth.Commands.ViewModels;

namespace Razdor.Identity.Module.Services.Auth.Commands;

public sealed record SignupCommand(
    string IdentityName,
    string Email,
    string Password
) : IIdentityCommand<AccessToken>;