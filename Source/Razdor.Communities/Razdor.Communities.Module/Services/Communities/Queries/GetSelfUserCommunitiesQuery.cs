using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Communities.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Services.Services.Communities.Queries;

public record struct GetSelfUserCommunitiesQuery :
    ICommunitiesQuery<IEnumerable<CommunityViewModel>>,
    IAuthorizationRequiredMessage;