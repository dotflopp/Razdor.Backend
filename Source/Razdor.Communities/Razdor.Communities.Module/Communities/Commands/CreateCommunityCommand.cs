using Razdor.Communities.Domain;
using Razdor.Communities.Services.Communities.ViewModels;
using Razdor.Communities.Services.Contracts;

namespace Razdor.Communities.Services.Communities.Commands;

public record CreateCommunityCommand(
    string Name
): ICommunitiesCommand<CommunityViewModel>;