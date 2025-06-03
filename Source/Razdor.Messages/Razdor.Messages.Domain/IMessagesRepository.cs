using Razdor.Shared.Domain.Repository;

namespace Razdor.Messages.Domain;

public interface IMessagesRepository : IUnitOfWorkRepository<Message>
{
    Message Add(Message message);
}