using Mediator;
using Razdor.Communities.Domain;
using Razdor.Shared.Domain;
using Razdor.Shared.Module;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.Media;
using Razdor.Shared.Module.Media.Exceptions;

namespace Razdor.Communities.Module.Services.Communities.Commands;

public class UploadCommunityAvatarCommandHandler(
    IFileStore store, 
    ICommunitiesRepository repository
) : ICommandHandler<UploadCommunityAvatarCommand>
{
    public async ValueTask<Unit> Handle(UploadCommunityAvatarCommand command, CancellationToken cancellationToken)
    {
        if (!command.ContentType.StartsWith("image/"))
            InvalidMediaTypeException.Throw(command.ContentType);
        
        CommunityAvatarPath path = new (command.CommunityId);
        MediaFileMeta meta = await store.UploadMediaFileAsync(path, command, cancellationToken);
        
        Community community = await repository.FindAsync(command.CommunityId, cancellationToken) ??
            throw new InvalidOperationException("The community was not found, despite the fact that the authorization check should have been passed.");

        community.Avatar = meta;
        await repository.UnitOfWork.SaveEntitiesAsync();
        return Unit.Value;
    }
}