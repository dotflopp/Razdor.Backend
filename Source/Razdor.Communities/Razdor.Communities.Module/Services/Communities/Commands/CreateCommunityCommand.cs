using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.ViewModels;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Communities.Commands;

public sealed record CreateCommunityCommand(
    string Name
) : ICommunitiesCommand<CommunityViewModel>, IAuthorizationRequiredMessage;