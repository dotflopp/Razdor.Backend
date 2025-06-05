using Mediator;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Services.Users.Queries.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Services.Users.Commands;

public class ChangeSelectedStatusCommandHandler(
    IUserRepository users,
    IRequestSenderContext sender
): ICommandHandler<ChangeSelectedStatusCommand>
{
    public async ValueTask<Unit> Handle(ChangeSelectedStatusCommand command, CancellationToken cancellationToken)
    {
        UserAccount? user = await users.FindByIdAsync(sender.User.Id, cancellationToken);
        if (user is null)
            UserNotFoundException.Throw(sender.User.Id);
        
        user.SelectedStatus = command.Status;
        
        await users.UnitOfWork.SaveEntitiesAsync();
        return Unit.Value;
    }
}