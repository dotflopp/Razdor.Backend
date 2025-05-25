using Mediator;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Authorization;
using Razdor.Communities.Services.Communities.Commands.ViewModels;
using Razdor.Communities.Services.Contracts;

namespace Razdor.Communities.Services.Communities.Commands;

public sealed record CreateInviteCommand(
    ulong CommunityId,
    TimeSpan? LifeTime
): ICommunitiesCommand<InviteViewModel>, IRequiredCommunityPermissionsMessage
{
    public UserPermissions RequiredPermissions => UserPermissions.InviteMembers;
}