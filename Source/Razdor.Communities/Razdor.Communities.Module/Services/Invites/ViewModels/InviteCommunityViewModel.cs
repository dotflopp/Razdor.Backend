using System.Text.Json.Serialization;
using Razdor.Communities.Domain;
using Razdor.Shared.Module;

namespace Razdor.Communities.Module.Services.Communities.ViewModels;

public record InviteCommunityViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id,
    string Name,
    string? Description,
    string? Avatar
)
{
    public static InviteCommunityViewModel From(Community community)
    {
        return new InviteCommunityViewModel(community.Id, community.Name, community.Description, community.Avatar?.SourceUrl);
    }
}