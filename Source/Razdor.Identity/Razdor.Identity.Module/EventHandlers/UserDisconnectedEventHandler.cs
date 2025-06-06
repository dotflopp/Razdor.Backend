using Mediator;
using Razdor.Gateways.PublicEvents;
using Razdor.Identity.Domain.Users;

namespace Razdor.Identity.Module.EventHandlers;

public class UserDisconnectedEventHandler(
    IUserRepository userRepository
): INotificationHandler<UserDisconnectedPublicEvent>
{
    public async ValueTask Handle(UserDisconnectedPublicEvent notification, CancellationToken cancellationToken)
    {
        UserAccount user = await userRepository.FindByIdAsync(notification.UserId, cancellationToken);
        user.IsOnline = false;
        
        await userRepository.UnitOfWork.SaveEntitiesAsync();
    }
}