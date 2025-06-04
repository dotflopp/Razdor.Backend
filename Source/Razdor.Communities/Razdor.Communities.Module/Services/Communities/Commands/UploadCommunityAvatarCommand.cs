using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Contracts;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Media;

namespace Razdor.Communities.Module.Services.Communities.Commands;

public record UploadCommunityAvatarCommand(
    ulong CommunityId,
    string FileName, 
    string ContentType,
    Stream Stream
) : ICommunitiesCommand, IRequiredCommunityPermissions, IMediaFile
{
    public UserPermissions RequiredPermissions => UserPermissions.ManageCommunity;
}