using Mediator;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Invites;

namespace Razdor.Communities.Module.Services.Invites.Queries;

public record GetInviteCommand(
    string InviteId  
): ICommunitiesCommand<InviteViewModel>;