using Razdor.Communities.Services.Contracts;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Services.Services.Invites.Commands;

public sealed record AcceptInviteCommand(
    string InviteId
) : ICommunitiesCommand, IAuthorizationRequiredMessage;