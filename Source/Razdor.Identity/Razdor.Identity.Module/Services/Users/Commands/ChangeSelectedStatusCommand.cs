using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Contracts;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Identity.Module.Services.Users.Commands;

public record ChangeSelectedStatusCommand(
    SelectedCommunicationStatus Status
): IIdentityCommand, IAuthorizationRequiredMessage;