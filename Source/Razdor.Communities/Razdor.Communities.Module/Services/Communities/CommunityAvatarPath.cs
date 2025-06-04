using Razdor.Shared.Module.Media;

namespace Razdor.Communities.Module.Services.Communities;

public record struct CommunityAvatarPath(
    ulong CommunityId    
) : IMediaContentPath
{
    public string AsString()
    {
        return $"/api/communities/{CommunityId}/avatar";
    }
}