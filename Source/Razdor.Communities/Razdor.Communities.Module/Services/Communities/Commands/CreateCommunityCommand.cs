using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Communities;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Communities.Commands;

public sealed record CreateCommunityCommand(
    string Name
) : ICommunitiesCommand<CommunityViewModel>, IAuthorizationRequiredMessage;