using System.Text.Json.Serialization;
using Razdor.Communities.Domain;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.PublicEvents.ViewModels.Communities;

public record CommunityViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong OwnerId,
    string Name,
    string? Avatar,
    string? Description,
    CommunityNotificationPolicy DefaultNotificationPolicy,
    List<RoleViewModel> Roles
)
{
    public static CommunityViewModel From(Community community)
    {
        return new CommunityViewModel(
            community.Id,
            community.OwnerId,
            community.Name,
            community.Avatar?.SourceUrl,
            community.Description,
            community.DefaultNotificationPolicy,
            community.Roles.Select(RoleViewModel.From).ToList()
        );
    }
}