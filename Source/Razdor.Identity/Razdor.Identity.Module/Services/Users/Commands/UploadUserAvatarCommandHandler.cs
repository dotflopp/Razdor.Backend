using Mediator;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.Media;
using Razdor.Shared.Module.Media.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Services.Users.Commands;

public class UploadUserAvatarCommandHandler(
    IRequestSenderContext sender,
    IUserRepository userRepository,
    IFileStore store    
): ICommandHandler<UploadUserAvatarCommand>
{
    public async ValueTask<Unit> Handle(UploadUserAvatarCommand command, CancellationToken cancellationToken)
    {
        if (!command.ContentType.StartsWith("image"))
            InvalidMediaTypeException.Throw(command.ContentType);
        
        UserAvatarPath path = new UserAvatarPath(sender.User.Id);
        MediaFileMeta meta = await store.UploadMediaFileAsync(path, command, cancellationToken);
        
        UserAccount? user = await userRepository.FindByIdAsync(sender.User.Id);
        if (user == null)
            throw new InvalidOperationException($"Authorized user with id {sender.User.Id} not found");

        user.Avatar = meta;
        
        await userRepository.UnitOfWork.SaveEntitiesAsync();
        return Unit.Value;
    }
}