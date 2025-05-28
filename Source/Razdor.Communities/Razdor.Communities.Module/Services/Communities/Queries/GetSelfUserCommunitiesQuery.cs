using Mediator;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Services.Communities.Queries;

public record struct GetSelfUserCommunitiesQuery() :
    ICommunitiesQuery<IEnumerable<CommunityViewModel>>,
    IAuthorizationRequiredMessage;
