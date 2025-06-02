using Razdor.Shared.Domain.Repository;

namespace Razdor.Messaging.Domain;

public interface IMessagesRepository : IUnitOfWorkRepository<Message>
{
    Message Add(Message message);
}