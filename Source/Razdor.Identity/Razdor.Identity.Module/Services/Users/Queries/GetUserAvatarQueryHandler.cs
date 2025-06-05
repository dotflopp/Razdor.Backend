using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Module.DataAccess;
using Razdor.Shared.Module.Media;
using Razdor.Shared.Module.Media.Exceptions;

namespace Razdor.Identity.Module.Services.Users.Queries;

public class GetUserAvatarQueryHandler(
    IFileStore store,
    IdentityDbContext context
) : ICommandHandler<GetUserAvatarQuery, MediaFile>
{
    public async ValueTask<MediaFile> Handle(GetUserAvatarQuery command, CancellationToken cancellationToken)
    {
        var user = await context.UserAccounts
            .AsNoTracking()
            .Where(x => x.Id == command.UserId)
            .Select(x => new {x.Id, x.Avatar})
            .FirstOrDefaultAsync(cancellationToken);

        if (user?.Avatar == null)
            MediaFileNotFoundException.Throw();
        
        UserAvatarPath path = new(user.Id);
        return await store.GetMediaFileAsync(path, user.Avatar, cancellationToken);
    }
}