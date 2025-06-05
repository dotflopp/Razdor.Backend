using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Communities.Module.Services.Communities.InternalQueries;

public record GetCommunityMemberPermissions(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong CommunityId, 
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId
): ICommunitiesQuery<(UserPermissions permissions, uint Priority)>;