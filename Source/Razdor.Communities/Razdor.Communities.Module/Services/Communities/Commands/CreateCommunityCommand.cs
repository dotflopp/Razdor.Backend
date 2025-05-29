using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Communities.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Services.Services.Communities.Commands;

public sealed record CreateCommunityCommand(
    string Name
) : ICommunitiesCommand<CommunityViewModel>, IAuthorizationRequiredMessage;