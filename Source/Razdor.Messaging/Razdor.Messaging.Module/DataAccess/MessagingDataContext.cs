using Microsoft.EntityFrameworkCore;
using Razdor.Messaging.Domain;

namespace Razdor.Messaging.Module.DataAccess;

public abstract class MessagingDataContext(DbContextOptions options): DbContext(options)
{
    public DbSet<Message> Messages { get; protected set; }
}