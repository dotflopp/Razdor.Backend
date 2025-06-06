using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Module.DataAccess;
using Razdor.Shared.Module.Media;
using Razdor.Shared.Module.Media.Exceptions;

namespace Razdor.Communities.Module.Services.Communities.Queries;

public class GetCommunityAvatarQueryHandler(
    IFileStore store,
    CommunitiesDbContext context
) : IQueryHandler<GetCommunityAvatarQuery, MediaFile>
{
    public async ValueTask<MediaFile> Handle(GetCommunityAvatarQuery query, CancellationToken cancellationToken)
    {
        var community = await context.Communities
            .AsNoTracking()
            .Where(x => x.Id == query.CommunityId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (community?.Avatar == null)
            MediaFileNotFoundException.Throw();

        CommunityAvatarPath path = new(query.CommunityId);
        return await store.GetMediaFileAsync(path, community.Avatar, cancellationToken);
    }
}