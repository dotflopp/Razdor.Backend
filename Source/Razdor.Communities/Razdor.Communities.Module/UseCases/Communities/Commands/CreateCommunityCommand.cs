using Razdor.Communities.Domain;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.Contracts;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Services.Communities.Commands;

public sealed record CreateCommunityCommand(
    string Name
): ICommunitiesCommand<CommunityViewModel>, IAuthorizationRequiredMessage;