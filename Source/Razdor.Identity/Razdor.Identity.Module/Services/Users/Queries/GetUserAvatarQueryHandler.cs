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

public class GetUserAvatarQueryHandler(
    IFileStore store,
    IdentityDbContext context
) : ICommandHandler<GetUserAvatarQuery, MediaFileViewModel>
{
    public async ValueTask<MediaFileViewModel> Handle(GetUserAvatarQuery command, CancellationToken cancellationToken)
    {
        var user = await context.UserAccounts
            .AsNoTracking()
            .Where(x => x.Id == command.UserId)
            .Select(x => new {x.Id, x.Avatar})
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            UserNotFoundException.Throw(command.UserId);
        
        AvatarPath path = new(user.Id);
        Stream stream = await store.GetFileStreamAsync(
            path.AsString(),
            cancellationToken
        );
        
        if (stream == Stream.Null)
            MediaFileNotFoundException.Throw();

        return new MediaFileViewModel(
            user.Avatar.FileName,
            user.Avatar.MediaType,
            stream
        );
    }
}