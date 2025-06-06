using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Invites;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Invites.Commands;

public sealed record AcceptInviteCommand(
    string InviteId
) : ICommunitiesCommand<InvitePreviewModel>, IAuthorizationRequiredMessage;