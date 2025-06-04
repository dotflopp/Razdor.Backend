using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Module.DataAccess;
using Razdor.Identity.Module.Services.Users.Avatars;
using Razdor.Identity.Module.Services.Users.Avatars.Queries;
using Razdor.Identity.Module.Users.Queries.Exceptions;
using Razdor.Messages.Module.Services.Commands.ViewModels;
using Razdor.Shared.Domain;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.Media;

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