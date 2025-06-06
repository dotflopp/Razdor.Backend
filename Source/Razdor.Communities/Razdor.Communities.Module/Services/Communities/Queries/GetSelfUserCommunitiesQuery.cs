using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Communities;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Communities.Queries;

public record struct GetSelfUserCommunitiesQuery :
    ICommunitiesQuery<IEnumerable<CommunityViewModel>>,
    IAuthorizationRequiredMessage;