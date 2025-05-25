using Mediator;
using Razdor.Communities.Services.Contracts;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Services.UseCases.Invites.Commands;

public sealed record AcceptInviteCommand(
    string InviteId
): ICommunitiesCommand, IAuthorizationRequiredMessage;