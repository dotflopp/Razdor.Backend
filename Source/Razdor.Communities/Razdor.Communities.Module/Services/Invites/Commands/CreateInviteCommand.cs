using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.Module.Services.Communities.ViewModels;

namespace Razdor.Communities.Module.Services.Invites.Commands;

public sealed record CreateInviteCommand(
    ulong CommunityId,
    TimeSpan? LifeTime
) : ICommunitiesCommand<InviteViewModel>, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.InviteMembers;
}