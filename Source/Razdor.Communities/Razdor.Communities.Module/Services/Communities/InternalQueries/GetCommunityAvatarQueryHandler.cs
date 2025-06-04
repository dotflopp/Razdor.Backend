using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Module.DataAccess;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.Media;

namespace Razdor.Communities.Module.Services.Communities.InternalQueries;

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
            .Select(x => new { x.Avatar })
            .FirstOrDefaultAsync(cancellationToken);
        
        if (community?.Avatar == null)
            MediaFileNotFoundException.Throw();

        CommunityAvatarPath path = new(query.CommunityId);
        return await store.GetMediaFileAsync(path, community.Avatar, cancellationToken);
    }
}