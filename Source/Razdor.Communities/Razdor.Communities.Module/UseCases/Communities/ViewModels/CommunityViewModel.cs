using Razdor.Communities.Domain;

namespace Razdor.Communities.Services.Communities.ViewModels;

public record CommunityViewModel(
    ulong Id,
    ulong OwnerId,
    string Name,
    string? Avatar,
    string? Description,
    CommunityNotificationPolicy DefaultNotificationPolicy,
    List<RoleViewModel> Role
)
{
    public static CommunityViewModel From(Community community)
    {
        return new CommunityViewModel(
            community.Id,
            community.OwnerId,
            community.Name,
            community.Avatar,
            community.Description,
            community.DefaultNotificationPolicy,
            community.Roles.Select(RoleViewModel.From).ToList()
        );
    }
};