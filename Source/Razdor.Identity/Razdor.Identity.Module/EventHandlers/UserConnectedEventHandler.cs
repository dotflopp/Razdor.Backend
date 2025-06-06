using Mediator;
using Razdor.Gateways.PublicEvents;
using Razdor.Identity.Domain.Users;

namespace Razdor.Identity.Module.EventHandlers;

public class UserConnectedEventHandler(
    IUserRepository userRepository
): INotificationHandler<UserConnectedPublicEvent>
{
    public async ValueTask Handle(UserConnectedPublicEvent notification, CancellationToken cancellationToken)
    {
        UserAccount user = await userRepository.FindByIdAsync(notification.UserId, cancellationToken);
        user.IsOnline = true;
        
        await userRepository.UnitOfWork.SaveEntitiesAsync();
    }
}