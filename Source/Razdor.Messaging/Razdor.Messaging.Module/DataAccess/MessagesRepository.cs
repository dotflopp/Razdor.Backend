using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Messaging.Domain;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Messaging.Module.DataAccess;

public class MessagesRepository(
    UnitOfWork<MessagingDataContext> unitOfWork,
    MessagingDataContext context
): IMessagesRepository
{
    private readonly MessagingDataContext _context = context;
    public IUnitOfWork UnitOfWork => unitOfWork;
    public Message Add(Message message)
    {
        EntityEntry<Message> entry = _context.Messages.Add(message);
        return entry.Entity;
    }
}