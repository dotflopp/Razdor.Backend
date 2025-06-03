using Mediator;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Services.Users.Avatars;
using Razdor.Shared.Domain;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Services.Users.Commands;

public class UploadUserAvatarCommandHandler(
    IRequestSenderContextAccessor sender,
    IUserRepository userRepository,
    IFileStore store    
): ICommandHandler<UploadUserAvatarCommand>
{
    public async ValueTask<Unit> Handle(UploadUserAvatarCommand command, CancellationToken cancellationToken)
    {
        if (!command.ContentType.StartsWith("image"))
            InvalidMediaTypeException.Throw(command.ContentType);
        
        AvatarPath path = new AvatarPath(
            sender.User.Id
        );
        
        bool isStored = await store.UploadFileAsync(
            path.AsString(),
            command.ContentType,
            command.AvatarStream,
            cancellationToken
        );
         
        if (!isStored)
            MediaFileNotUploadedException.Throw();

        UserAccount? user = await userRepository.FindByIdAsync(sender.User.Id);
         
        if (user == null)
            throw new InvalidOperationException($"Authorized user with id {sender.User.Id} not found");

        user.Avatar = new MediaFileMeta(
            command.FileName,
            path.AsString(),
            command.ContentType,
            command.AvatarStream.Length
        );

        await userRepository.UnitOfWork.SaveEntitiesAsync();
        return Unit.Value;
    }
}