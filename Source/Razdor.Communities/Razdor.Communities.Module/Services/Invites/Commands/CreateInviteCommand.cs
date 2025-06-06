using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Communities.Module.Contracts;
using Razdor.Communities.PublicEvents.ViewModels.Invites;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Authorization;

namespace Razdor.Communities.Module.Services.Invites.Commands;

public sealed record CreateInviteCommand(
    ulong CommunityId,
    TimeSpan? LifeTime
) : ICommunitiesCommand<InvitePreviewModel>, IRequiredCommunityPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.InviteMembers;
}