using Mediator;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.ViewModels;

namespace Razdor.Communities.Module.Services.Invites.Queries;

public record GetInviteCommand(
    string InviteId  
): ICommunitiesCommand<InviteViewModel>;