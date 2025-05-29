using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Services.Authorization;
using Razdor.Communities.Services.Contracts;
using Razdor.Communities.Services.Services.Communities.ViewModels;

namespace Razdor.Communities.Services.Services.Invites.Commands;

public sealed record CreateInviteCommand(
    ulong CommunityId,
    TimeSpan? LifeTime
) : ICommunitiesCommand<InviteViewModel>, IRequiredCommunityPermissionsMessage
{
    public UserPermissions RequiredPermissions => UserPermissions.InviteMembers;
}