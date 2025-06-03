using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Messages.Domain;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Messages.Module.DataAccess;

public class MessagesRepository(
    UnitOfWork<MessagesDbContext> unitOfWork,
    MessagesDbContext context
): IMessagesRepository
{
    private readonly MessagesDbContext _context = context;
    public IUnitOfWork UnitOfWork => unitOfWork;
    public Message Add(Message message)
    {
        EntityEntry<Message> entry = _context.Messages.Add(message);
        return entry.Entity;
    }
}